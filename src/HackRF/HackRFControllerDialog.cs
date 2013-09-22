using System;
using System.Globalization;
using System.Windows.Forms;
using SDRSharp.Radio;
using System.Timers;

namespace SDRSharp.HackRF
{
    public partial class HackRFControllerDialog : Form
    {
        private readonly HackRFIO _owner;
        private bool _initialized;
        private const int LNAGainStep = 8;
        private const int VGAGainStep = 2;
        private const int IFFreqOffset = 2400;
        private static System.Timers.Timer configureDelay = new System.Timers.Timer();

        public HackRFControllerDialog(HackRFIO owner)
        {
            InitializeComponent();

            _owner = owner;
            var devices = DeviceDisplay.GetActiveDevices();
            deviceComboBox.Items.Clear();
            deviceComboBox.Items.AddRange(devices);

            samplerateComboBox.SelectedIndex = Utils.GetIntSetting("HackRFSampleRate", 3);
            offsetTuningCheckBox.Checked = Utils.GetBooleanSetting("HackRFOffsetTuning");
            rtlAgcCheckBox.Checked = Utils.GetBooleanSetting("HackRFChipAgc");
            tunerAmpCheckBox.Checked = Utils.GetBooleanSetting("HackRFTunerAmp");
            tunerLNAGainTrackBar.Value = Utils.GetIntSetting("LNATunerGain", 3);
            tunerVGAGainTrackBar.Value = Utils.GetIntSetting("VGATunerGain", 12);
            tunerIFFreq.Value = Utils.GetIntSetting("HackRFIFFreq", 0);
            gainLNALabel.Text = (tunerLNAGainTrackBar.Value * LNAGainStep) + " dB";
            gainVGALabel.Text = (tunerVGAGainTrackBar.Value * VGAGainStep) + " dB";
            IFLabel.Text = (tunerIFFreq.Value + IFFreqOffset) + " MHz";
                    
            //gainLabel.Visible = tunerAmpCheckBox.Enabled && !tunerAmpCheckBox.Checked;
            //tunerGainTrackBar.Enabled = tunerAmpCheckBox.Enabled && !tunerAmpCheckBox.Checked;
            gainLNALabel.Visible = tunerLNAGainTrackBar.Enabled = true;
            gainVGALabel.Visible = tunerVGAGainTrackBar.Enabled = true;
            IFLabel.Visible = tunerIFFreq.Enabled = true;
            offsetTuningCheckBox.Enabled = false;
            tunerAmpCheckBox.Enabled = true;

            _initialized = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HackRFControllerDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void HackRFControllerDialog_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = Visible;
            if (Visible)
            {
                samplerateComboBox.Enabled = !_owner.Device.IsStreaming;
                deviceComboBox.Enabled = !_owner.Device.IsStreaming;
                
                if (!_owner.Device.IsStreaming)
                {
                    var devices = DeviceDisplay.GetActiveDevices();
                    deviceComboBox.Items.Clear();
                    deviceComboBox.Items.AddRange(devices);

                    for (var i = 0; i < devices.Length; i++)
                    {
                        if (devices[i].Index == ((DeviceDisplay) deviceComboBox.Items[i]).Index)
                        {
                            _initialized = false;
                            deviceComboBox.SelectedIndex = i;
                            _initialized = true;
                            break;
                        }
                    }
                }
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            samplerateComboBox.Enabled = !_owner.Device.IsStreaming;
            deviceComboBox.Enabled = !_owner.Device.IsStreaming;
        }

        private void deviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            var deviceDisplay = (DeviceDisplay) deviceComboBox.SelectedItem;
            if (deviceDisplay != null)
            {
                try
                {
                    _owner.SelectDevice(deviceDisplay.Index);
                }
                catch (Exception ex)
                {
                    deviceComboBox.SelectedIndex = -1;
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void samplerateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            var samplerateString = samplerateComboBox.Items[samplerateComboBox.SelectedIndex].ToString().Split(' ')[0];
            var sampleRate = double.Parse(samplerateString, CultureInfo.InvariantCulture);
            _owner.Device.Samplerate = (uint) (sampleRate * 1000000.0);
            Utils.SaveSetting("HackRFSampleRate", samplerateComboBox.SelectedIndex);
        }


        private void rtlAgcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            _owner.Device.UseHackRFAGC = rtlAgcCheckBox.Checked;
            Utils.SaveSetting("HackRFChipAgc", rtlAgcCheckBox.Checked);
        }

        private void offsetTuningCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _owner.Device.UseOffsetTuning = offsetTuningCheckBox.Checked;
            Utils.SaveSetting("HackRFOffsetTuning", offsetTuningCheckBox.Checked);
        }

        public void ConfigureGUI()
        {
            offsetTuningCheckBox.Enabled = _owner.Device.SupportsOffsetTuning;

            for (var i = 0; i < deviceComboBox.Items.Count; i++)
            {
                var deviceDisplay = (DeviceDisplay) deviceComboBox.Items[i];
                if (deviceDisplay.Index == _owner.Device.Index)
                {
                    deviceComboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        public void ConfigureDevice()
        {
            tunerVGAGainTrackBar_Scroll(null, null);
            tunerLNAGainTrackBar_Scroll(null, null);
            tunerIFFreq_Scroll(null, null);
            samplerateComboBox_SelectedIndexChanged(null, null);
            offsetTuningCheckBox_CheckedChanged(null, null);
            tunerAmpCheckBox_CheckedChanged(null, null);
            if (!_owner.Device.IsStreaming)
            {
                // A timer is required since for whatever reason, the amp (and assumingly other settings)
                // are not properly applied when ConfigureDevice() is called by SDRSharp
                configureDelay.Interval = 10;
                configureDelay.Elapsed += new ElapsedEventHandler(configureDelayHandler);
                configureDelay.Start();
            }
            /*
            tunerAgcCheckBox_CheckedChanged(null, null);
            if (!tunerAgcCheckBox.Checked)
            {
                tunerGainTrackBar_Scroll(null, null);
            }
            */
        }

        private void configureDelayHandler(object source, ElapsedEventArgs e)
        {
            if (_owner.Device.IsStreaming)
            {
                configureDelay.Interval = 10000;
                configureDelay.Stop();
                ConfigureDevice();
            }
        }

        private void tunerAmpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            _owner.Device.UseTunerAMP = tunerAmpCheckBox.Checked;
            Utils.SaveSetting("HackRFTunerAmp", tunerAmpCheckBox.Checked);
        }

        private void tunerVGAGainTrackBar_Scroll(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            _owner.Device.VGAGain = (tunerVGAGainTrackBar.Value * VGAGainStep);
            gainVGALabel.Text = _owner.Device.VGAGain + " dB";
            Utils.SaveSetting("VGATunerGain", tunerVGAGainTrackBar.Value);
        }

        private void tunerLNAGainTrackBar_Scroll(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            _owner.Device.LNAGain = (tunerLNAGainTrackBar.Value * LNAGainStep);
            gainLNALabel.Text = _owner.Device.LNAGain + " dB";
            Utils.SaveSetting("LNATunerGain", tunerLNAGainTrackBar.Value);
        }

        private void tunerIFFreq_Scroll(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            _owner.Device.IFFreq = (tunerIFFreq.Value + IFFreqOffset);
            IFLabel.Text = _owner.Device.IFFreq + " MHz";
            Utils.SaveSetting("HackRFIFFreq", tunerIFFreq.Value);
        }
    }

    public class DeviceDisplay
    {
        public uint Index { get; private set; }
        public string Name { get; set; }

        public static DeviceDisplay[] GetActiveDevices()
        {
            /*
            var count = NativeMethods.rtlsdr_get_device_count();
             */
            //string namestr = "HackRF Device";
            var count = 1;
            var result = new DeviceDisplay[count];
			
			var name = NativeMethods.hackrf_board_id_name(1);
            result[0] = new DeviceDisplay { Index = 0, Name = name };
            /*
            for (var i = 0u; i < count; i++)
            {
                var name = NativeMethods.rtlsdr_get_device_name(i);
                result[i] = new DeviceDisplay { Index = i, Name = name };
            }
            */
            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
