using System;
using System.Runtime.InteropServices;
using System.Threading;
using SDRSharp.Radio;

namespace SDRSharp.HackRF
{
    public enum SamplingMode
    {
        Quadrature = 0,
        DirectSamplingI,
        DirectSamplingQ
    }

    public unsafe sealed class HackRFDevice : IDisposable
    {
        private const uint DefaultFrequency = 105500000;
        private const int DefaultSamplerate = 10000000; /* 10MHz */

        private static readonly float* _lutPtr;
        private static readonly UnsafeBuffer _lutBuffer = UnsafeBuffer.Create(256, sizeof(float));

        private readonly uint _index;
        private IntPtr _dev;
        private readonly string _name;
        private readonly int[] _supportedGains;
        private bool _useTunerAGC = true;
        private bool _useHackRFAGC;
        private int _tunerGain;
        private long _centerFrequency = DefaultFrequency;
        private uint _sampleRate = DefaultSamplerate;
        private int _frequencyCorrection;
        private SamplingMode _samplingMode;
        private bool _useOffsetTuning;
        private readonly bool _supportsOffsetTuning;

        private GCHandle _gcHandle;
        private UnsafeBuffer _iqBuffer;
        private Complex* _iqPtr;
        private bool _rxIsRunning;
        private readonly SamplesAvailableEventArgs _eventArgs = new SamplesAvailableEventArgs();
        private static readonly hackrf_sample_block_cb_fn _HackRFCallback = HackRFSamplesAvailable;
        private static readonly uint _readLength = (uint)Utils.GetIntSetting("HackRFBufferLength", 16 * 1024);

        static HackRFDevice()
        {
            _lutPtr = (float*)_lutBuffer;

            const float scale = 1.0f / 127.0f;
            for (var i = 0; i < 256; i++)
            {
                _lutPtr[i] = (i - 128) * scale;
            }
        }

        public HackRFDevice()
        {
            var r = NativeMethods.hackrf_init();
            if (r != 0)
            {
                throw new ApplicationException("Cannot init HackRF device. Is the device locked somewhere?");
            }

            r = NativeMethods.hackrf_open(out _dev);
            if (r != 0)
            {
                throw new ApplicationException("Cannot open HackRF device. Is the device locked somewhere?");
            }
            /*
            var count = _dev == IntPtr.Zero ? 0 : NativeMethods.rtlsdr_get_tuner_gains(_dev, null);
            if (count < 0)
            {
                count = 0;
            }
            */
            var count = 0;

            //_supportsOffsetTuning = NativeMethods.rtlsdr_set_offset_tuning(_dev, 0) != -2;
            _supportsOffsetTuning = false;
            _supportedGains = new int[count];
            /*
            if (count >= 0)
            {
                NativeMethods.rtlsdr_get_tuner_gains(_dev, _supportedGains);
            }
            */
            _name = "HackRF Jawbreaker"; // NativeMethods.rtlsdr_get_device_name(_index);
            _gcHandle = GCHandle.Alloc(this);
        }

        ~HackRFDevice()
        {
            Dispose();
        }

        public void Dispose()
        {
            Stop();
            var r = NativeMethods.hackrf_close(_dev);
            if (r != 0)
            {
                //throw new ApplicationException("Cannot close HackRF device.");
            }
            NativeMethods.hackrf_exit();
            if (_gcHandle.IsAllocated)
            {
                _gcHandle.Free();
            }
            _dev = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        public event SamplesAvailableDelegate SamplesAvailable;

        public void Start()
        {
            uint baseband_filter_bw_hz;
            var r = 0;

            if (_rxIsRunning)
            {
                throw new ApplicationException("Start() Already running");
            }

            r = NativeMethods.hackrf_set_sample_rate(_dev, _sampleRate);
            if (r != 0)
            {
                throw new ApplicationException("hackrf_sample_rate_set error");
            }

            r = NativeMethods.hackrf_set_amp_enable(_dev, 1); /* 0 = OFF, 1= ON */
            if (r != 0)
            {
                throw new ApplicationException("hackrf_set_amp_enable error");
            }

            baseband_filter_bw_hz = NativeMethods.hackrf_compute_baseband_filter_bw_round_down_lt(_sampleRate);
            //baseband_filter_bw_hz = 5000000; /* Force 5MHz */
            r = NativeMethods.hackrf_set_baseband_filter_bandwidth(_dev, baseband_filter_bw_hz);
            if (r != 0)
            {
                throw new ApplicationException("hackrf_baseband_filter_bandwidth_set error");
            }

            r = NativeMethods.hackrf_start_rx(_dev, _HackRFCallback, (IntPtr)_gcHandle);
            if (r != 0)
            {
                throw new ApplicationException("hackrf_start_rx error");
            }

            r = NativeMethods.hackrf_is_streaming(_dev);
            if (r != 1)
            {
                throw new ApplicationException("hackrf_is_streaming() Error");
            }

            _rxIsRunning = true;
        }

        public void Stop()
        {
            if (!_rxIsRunning)
            {
                return;
            }

            var r = NativeMethods.hackrf_stop_rx(_dev);
            if (r != 0)
            {
                //throw new ApplicationException("hackrf_stop_rx error");
            }

            _rxIsRunning = false;
        }

        public uint Index
        {
            get { return _index; }
        }

        public string Name
        {
            get { return _name; }
        }

        public uint Samplerate
        {
            get
            {
                return _sampleRate;
            }
            set
            {
                _sampleRate = value;
                if (_dev != IntPtr.Zero)
                {
                    NativeMethods.hackrf_set_sample_rate(_dev, _sampleRate);
                }
            }
        }

        public long Frequency
        {
            get
            {
                return _centerFrequency;
            }
            set
            {
                _centerFrequency = value;
                if (_dev != IntPtr.Zero)
                {
                    NativeMethods.hackrf_set_freq(_dev, _centerFrequency);
                }
            }
        }

        public bool UseHackRFAGC
        {
            get { return _useHackRFAGC; }
            set
            {
                _useHackRFAGC = value;
                /* TODO code HackRF AGC */
                /*
                if (_dev != IntPtr.Zero)
                {
                    NativeMethods.rtlsdr_set_agc_mode(_dev, _useHackRFAGC ? 1 : 0);
                }
                */
            }
        }

        public bool UseTunerAGC
        {
            get { return _useTunerAGC; }
            set
            {
                _useTunerAGC = value;
                /* TODO code HackRF Tuner AGC */
                /*
                if (_dev != IntPtr.Zero)
                {
                    NativeMethods.rtlsdr_set_tuner_gain_mode(_dev, _useTunerAGC ? 0 : 1);
                }
                */
            }
        }

        public SamplingMode SamplingMode
        {
            get { return _samplingMode; }
            set
            {
                _samplingMode = value;
                /* TODO code HackRF Direct Sampling TBD */
                /*
                if (_dev != IntPtr.Zero)
                {
                    NativeMethods.rtlsdr_set_direct_sampling(_dev, (int) _samplingMode);
                }
                */
            }
        }

        public bool SupportsOffsetTuning
        {
            get { return _supportsOffsetTuning; }
        }

        public bool UseOffsetTuning
        {
            get { return _useOffsetTuning; }
            set
            {
                _useOffsetTuning = value;
                /* TODO code HackRF Offset Tuning TBD */
                /*
                if (_dev != IntPtr.Zero)
                {
                    NativeMethods.rtlsdr_set_offset_tuning(_dev, _useOffsetTuning ? 1 : 0);
                }
                */
            }
        }

        public int[] SupportedGains
        {
            get { return _supportedGains; }
        }

        public int Gain
        {
            get { return _tunerGain; }
            set
            {
                _tunerGain = value;
                /* TODO code HackRF Tuner Gain TBD */
                /*
                if (_dev != IntPtr.Zero)
                {
                    NativeMethods.rtlsdr_set_tuner_gain(_dev, _tunerGain);
                }
                */
            }
        }

        public int FrequencyCorrection
        {
            get
            {
                return _frequencyCorrection;
            }
            set
            {
                _frequencyCorrection = value;
                /* TODO code HackRF Freq Correction TBD */
                /*
                if (_dev != IntPtr.Zero)
                {
                    NativeMethods.rtlsdr_set_freq_correction(_dev, _frequencyCorrection);
                }
                */
            }
        }
        /*
        public RtlSdrTunerType TunerType
        {
            get
            {
                return _dev == IntPtr.Zero ? RtlSdrTunerType.Unknown : NativeMethods.rtlsdr_get_tuner_type(_dev);
            }
        }
        */

        public bool IsStreaming
        {
            get
            {
                return _rxIsRunning;
            }
        }

        #region Streaming methods

        private void ComplexSamplesAvailable(Complex* buffer, int length)
        {
            if (SamplesAvailable != null)
            {
                _eventArgs.Buffer = buffer;
                _eventArgs.Length = length;
                SamplesAvailable(this, _eventArgs);
            }
        }

        private static int HackRFSamplesAvailable(hackrf_transfer* ptr)
        {
            byte* buf = ptr->buffer;
            int len = ptr->buffer_length;
            IntPtr ctx = ptr->rx_ctx;

            var gcHandle = GCHandle.FromIntPtr(ctx);
            if (!gcHandle.IsAllocated)
            {
                return -1;
            }
            var instance = (HackRFDevice)gcHandle.Target;

            var sampleCount = (int)len / 2;
            if (instance._iqBuffer == null || instance._iqBuffer.Length != sampleCount)
            {
                instance._iqBuffer = UnsafeBuffer.Create(sampleCount, sizeof(Complex));
                instance._iqPtr = (Complex*)instance._iqBuffer;
            }

            var ptrIq = instance._iqPtr;
            for (var i = 0; i < sampleCount; i++)
            {
                ptrIq->Imag = _lutPtr[*buf++];
                ptrIq->Real = _lutPtr[*buf++];
                ptrIq++;
            }

            instance.ComplexSamplesAvailable(instance._iqPtr, instance._iqBuffer.Length);
            return 0;
        }

        #endregion
    }

    public delegate void SamplesAvailableDelegate(object sender, SamplesAvailableEventArgs e);

    public unsafe sealed class SamplesAvailableEventArgs : EventArgs
    {
        public int Length { get; set; }
        public Complex* Buffer { get; set; }
    }
}
