using System;
using System.Windows.Forms;
using SDRSharp.Radio;

namespace SDRSharp.HackRF
{
    public unsafe class HackRFIO : IFrontendController, IDisposable
    {
        private readonly HackRFControllerDialog _gui;
        private HackRFDevice _HackRFDevice;
        private uint _frequency = 105500000;
        private Radio.SamplesAvailableDelegate _callback;

        public HackRFIO()
        {
            _gui = new HackRFControllerDialog(this);
        }

        ~HackRFIO()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_gui != null)
            {
                _gui.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        public void SelectDevice(uint index)
        {
            Close();
            _HackRFDevice = new HackRFDevice();
            _HackRFDevice.SamplesAvailable += HackRFDevice_SamplesAvailable;
            _HackRFDevice.Frequency = _frequency;
            _gui.ConfigureGUI();
            _gui.ConfigureDevice();
        }

        public HackRFDevice Device
        {
            get { return _HackRFDevice; }
        }

        public void Open()
        {
            var devices = DeviceDisplay.GetActiveDevices();
            foreach (var device in devices)
            {
                try
                {
                    SelectDevice(device.Index);
                    return;
                }
                catch (ApplicationException)
                {
                    // Just ignore it
                }
            }
            if (devices.Length > 0)
            {
                throw new ApplicationException(devices.Length + " compatible devices have been found but are all busy");
            }
            throw new ApplicationException("No compatible devices found");
        }

        public void Close()
        {
            if (_HackRFDevice != null)
            {
                _HackRFDevice.Stop();
                _HackRFDevice.SamplesAvailable -= HackRFDevice_SamplesAvailable;
                _HackRFDevice.Dispose();
                _HackRFDevice = null;
            }
        }

        public void Start(Radio.SamplesAvailableDelegate callback)
        {
            if (_HackRFDevice == null)
            {
                throw new ApplicationException("No device selected");
            }
            _callback = callback;
            try
            {
                _HackRFDevice.Start();
            }
            catch
            {
                Open();
                _HackRFDevice.Start();
            }
        }

        public void Stop()
        {
            _HackRFDevice.Stop();
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
            get { return _HackRFDevice == null ? 0.0 : _HackRFDevice.Samplerate; }
        }

        public long Frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = (uint) value;
                if (_HackRFDevice != null)
                {
                    _HackRFDevice.Frequency = _frequency;
                }
            }
        }

        private void HackRFDevice_SamplesAvailable(object sender, SamplesAvailableEventArgs e)
        {
            _callback(this, e.Buffer, e.Length);
        }
    }
}
