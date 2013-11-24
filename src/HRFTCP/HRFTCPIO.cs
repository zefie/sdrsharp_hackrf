using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using SDRSharp.Radio;

namespace SDRSharp.HRFTCP
{
    public unsafe class HRFTCPIO : IFrontendController, IDisposable
    {
        private const int DongleInfoLength = 12;
        private const string DefaultHost = "127.0.0.1";
        private const short DefaultPort = 1234;
        private const uint DefaultFrequency = 100000000;
        private readonly static int _bufferSize = Utils.GetIntSetting("HRFTCPBufferLength", 32 * 1024);
        
        #region Native rtl_tcp Commands

        private const byte CMD_SET_FREQ = 0x1;
        private const byte CMD_SET_SAMPLE_RATE = 0x2;
        private const byte CMD_SET_TUNER_GAIN_MODE = 0x3;
        private const byte CMD_SET_GAIN = 0x4;
        private const byte CMD_SET_FREQ_COR = 0x5;
        private const byte CMD_SET_AGC_MODE = 0x8;
        private const byte CMD_SET_TUNER_GAIN_INDEX = 0xd;
        private const byte CMD_SET_TUNER_AMP = 0xb0;
        private const byte CMD_SET_TUNER_VGA_GAIN = 0xb1;
        private const byte CMD_SET_TUNER_LNA_GAIN = 0xb2;
        private const byte CMD_SET_TUNER_IF = 0xb3;

        #endregion

        private static readonly float* _lutPtr;
        private static readonly UnsafeBuffer _lutBuffer = UnsafeBuffer.Create(256, sizeof(float));

        private long _frequency = DefaultFrequency;
        private double _sampleRate;
        private string _host;
        private int _port;
        private bool _useTunerAMP;
        private int _tunerGainLNA;
        private int _tunerGainVGA;
        private int _tunerIF;
        private SamplesAvailableDelegate _callback;
        private Thread _sampleThread;
        private UnsafeBuffer _iqBuffer;        
        private Complex* _iqBufferPtr;
        private Socket _s;
        private readonly byte [] _cmdBuffer = new byte[5];
        private readonly HRFTCPSettings _gui;
                            
        #region Public Properties

        public bool IsStreaming
        {
            get { return _sampleThread != null; }
        }

        public bool IsSoundCardBased
        {
            get { return false; }
        }

        public string SoundCardHint
        {
            get { return string.Empty; }
        }

        public void ShowSettingGUI(IWin32Window parent)
        {
            _gui.Show();
        }

        public void HideSettingGUI()
        {
            _gui.Hide();
        }

        public double Samplerate
        {
            get { return _sampleRate; }
            set
            {
                _sampleRate = value;
                SendCommand(CMD_SET_SAMPLE_RATE, (uint) _sampleRate);
            }
        }

        public long Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                SendCommand(CMD_SET_FREQ, (ulong)_frequency);
            }
        }

        public bool UseTunerAMP
        {
            get { return _useTunerAMP; }
            set
            {
                _useTunerAMP = value;
                if (IsStreaming)
                {
                    SendCommand(CMD_SET_TUNER_AMP, _useTunerAMP ? 1 : 0);
                }
            }
        }

        public int tunerGainVGA
        {
            get { return _tunerGainVGA; }
            set
            {
                _tunerGainVGA = value;
                if (IsStreaming)
                {
                    SendCommand(CMD_SET_TUNER_VGA_GAIN, (uint)_tunerGainVGA);
                }
            }
        }
        public int tunerGainLNA
        {
            get { return _tunerGainLNA; }
            set
            {
                _tunerGainLNA = value;
                if (IsStreaming)
                {
                    SendCommand(CMD_SET_TUNER_LNA_GAIN, (uint)_tunerGainLNA);
                }
            }
        }
        public int tunerIF
        {
            get { return _tunerIF; }
            set
            {
                _tunerIF = value;
                if (IsStreaming)
                {
                    SendCommand(CMD_SET_TUNER_IF, (ulong)_tunerIF);
                }
            }
        }

        #endregion

        static HRFTCPIO()
        {
            _lutPtr = (float*) _lutBuffer;

            const float scale = 1.0f / 127.5f;
            for (var i = 0; i < 256; i++)
            {
                _lutPtr[i] = (i - 127.5f) * scale;
            }
        }

        public HRFTCPIO()
        {
            _gui = new HRFTCPSettings(this);
            _gui.Hostname = Utils.GetStringSetting("HRFTCPHost", DefaultHost);
            _gui.Port = Utils.GetIntSetting("HRFTCPPort", DefaultPort);
            _frequency = DefaultFrequency;          
        }

        ~HRFTCPIO()
        {
            Dispose();
        }
                
        public void Dispose()
        {
            if (_iqBuffer != null)
            {
                _iqBuffer.Dispose();
                _iqBuffer = null;
                _iqBufferPtr = null;
            }
            if (_gui != null)
            {
                _gui.Dispose();                
            }
            GC.SuppressFinalize(this);
        }

        public void Open()
        {
        }

        public void Close()
        {
            if (_s != null)
            {
                _s.Close();
                _s = null;
            }
        }

        public void Start(SamplesAvailableDelegate callback)
        {
            _callback = callback;
            _host = _gui.Hostname;
            _port = _gui.Port;
            _s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _s.NoDelay = true;
            _s.Connect(_host, _port);

            var dongleInfo = new byte[DongleInfoLength];

            var length = _s.Receive(dongleInfo, 0, DongleInfoLength, SocketFlags.None);
            if (length > 0)
            {
                ParseDongleInfo(dongleInfo);
            }
            
            SendCommand(CMD_SET_SAMPLE_RATE, (uint)_sampleRate);
            SendCommand(CMD_SET_FREQ, (uint)_frequency);
            SendCommand(CMD_SET_TUNER_AMP, (uint)(_useTunerAMP ? 1 : 0));
            SendCommand(CMD_SET_TUNER_VGA_GAIN, _tunerGainVGA);
            SendCommand(CMD_SET_TUNER_LNA_GAIN, _tunerGainLNA);
            SendCommand(CMD_SET_TUNER_IF, _tunerIF);

            _sampleThread = new Thread(RecieveSamples);            
            _sampleThread.Start();
            
            Utils.SaveSetting("HRFTCPHost", _host);
            Utils.SaveSetting("HRFTCPPort", _port);
        }

        public void Stop()
        {
            Close();
            if (_sampleThread != null)
            {
                _sampleThread.Join();
                _sampleThread = null;
            }
            _callback = null;           
        }

        #region Private Methods

        private void ParseDongleInfo(byte[] buffer)
        {
                return;                 
        }

        public bool SendCommand(byte cmd, byte[] val)
        {
            if (_s == null || val.Length < 4)
            {
                return false;
            }
            
            _cmdBuffer[0] = cmd;
            _cmdBuffer[1] = val[3]; //Network byte order
            _cmdBuffer[2] = val[2];
            _cmdBuffer[3] = val[1];
            _cmdBuffer[4] = val[0];
            try
            {
                _s.Send(_cmdBuffer);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void SendCommand(byte cmd, UInt64 val)
        {
            var valBytes = BitConverter.GetBytes(val);
            SendCommand(cmd, valBytes);
        }

        private void SendCommand(byte cmd, UInt32 val)
        {
            var valBytes = BitConverter.GetBytes(val);
            SendCommand(cmd, valBytes);
        }

        private void SendCommand(byte cmd, Int32 val)
        {
            var valBytes = BitConverter.GetBytes(val);
            SendCommand(cmd, valBytes);
        }

        #endregion

        #region Worker Thread

        private void RecieveSamples()
        {
            var recBuffer = new byte[_bufferSize];
            var recUnsafeBuffer = UnsafeBuffer.Create(recBuffer);
            var recPtr = (byte*) recUnsafeBuffer;
            _iqBuffer = UnsafeBuffer.Create(_bufferSize / 2, sizeof(Complex));
            _iqBufferPtr = (Complex*) _iqBuffer;
            var offs = 0;                        
            while (_s != null && _s.Connected)
            {
                try
                {
                    var bytesRec = _s.Receive(recBuffer, offs, _bufferSize - offs, SocketFlags.None);
                    var totalBytes = offs + bytesRec;
                    offs = totalBytes % 2; //Need to correctly handle the hypothetical case where we somehow get an odd number of bytes
                    ProcessSamples(recPtr, totalBytes - offs); //This might work.
                    if (offs == 1)
                    {
                        recPtr[0] = recPtr[totalBytes - 1];
                    }
                }
                catch
                {
                    Close();
                    break;
                }
            }            
        }

        private void ProcessSamples(byte* rawPtr, int len)
        {
            var sampleCount = len / 2;

            var ptr = _iqBufferPtr;
            for (var i = 0; i < sampleCount; i++)
            {
                ptr->Imag = _lutPtr[*rawPtr++];
                ptr->Real = _lutPtr[*rawPtr++];
                ptr++;
            }
            if (_callback != null)
            {
                _callback(this, _iqBufferPtr, sampleCount);
            }
        }

        #endregion
    }
}
