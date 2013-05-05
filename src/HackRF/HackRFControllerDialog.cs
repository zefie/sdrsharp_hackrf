using System;
using System.Globalization;
using System.Windows.Forms;
using SDRSharp.Radio;

namespace SDRSharp.HackRF
{
    public partial class HackRFControllerDialog : Form
    {
        private readonly HackRFIO _owner;
        private bool _initialized;

        public HackRFControllerDialog(HackRFIO owner)
        {
            InitializeComponent();

            _owner = owner;
            var devices = DeviceDisplay.GetActiveDevices();
            deviceComboBox.Items.Clear();
            deviceComboBox.Items.AddRange(devices);

            frequencyCorrectionNumericUpDown.Value = Utils.GetIntSetting("HackRFFrequencyCorrection", 0);
            samplerateComboBox.SelectedIndex = Utils.GetIntSetting("HackRFSampleRate", 3);
            samplingModeComboBox.SelectedIndex = Utils.GetIntSetting("HackRFSamplingMode", 0);
            offsetTuningCheckBox.Checked = Utils.GetBooleanSetting("HackRFOffsetTuning");
            rtlAgcCheckBox.Checked = Utils.GetBooleanSetting("HackRFChipAgc");
            tunerAgcCheckBox.Checked = Utils.GetBooleanSetting("HackRFTunerAgc");
            tunerGainTrackBar.Value = Utils.GetIntSetting("HackRFTunerGain", 0);
            
            tunerAgcCheckBox.Enabled = samplingModeComboBox.SelectedIndex == 0;
            gainLabel.Visible = tunerAgcCheckBox.Enabled && !tunerAgcCheckBox.Checked;
            tunerGainTrackBar.Enabled = tunerAgcCheckBox.Enabled && !tunerAgcCheckBox.Checked;
            offsetTuningCheckBox.Enabled = samplingModeComboBox.SelectedIndex == 0;

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
                samplingModeComboBox.Enabled = !_owner.Device.IsStreaming;

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
            samplingModeComboBox.Enabled = !_owner.Device.IsStreaming;
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

        private void tunerGainTrackBar_Scroll(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            var gain = _owner.Device.SupportedGains[tunerGainTrackBar.Value];
            _owner.Device.Gain = gain;
            gainLabel.Text = gain / 10.0 + " dB";
            Utils.SaveSetting("HackRFTunerGain", tunerGainTrackBar.Value);
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

        private void tunerAgcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            tunerGainTrackBar.Enabled = tunerAgcCheckBox.Enabled && !tunerAgcCheckBox.Checked;
            _owner.Device.UseTunerAGC = tunerAgcCheckBox.Checked;
            gainLabel.Visible = tunerAgcCheckBox.Enabled && !tunerAgcCheckBox.Checked;
            if (!tunerAgcCheckBox.Checked)
            {
                tunerGainTrackBar_Scroll(null, null);
            }
            Utils.SaveSetting("HackRFTunerAgc", tunerAgcCheckBox.Checked);
        }

        private void frequencyCorrectionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }
            _owner.Device.FrequencyCorrection = (int) frequencyCorrectionNumericUpDown.Value;
            Utils.SaveSetting("HackRFFrequencyCorrection", frequencyCorrectionNumericUpDown.Value.ToString());
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

        private void samplingModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_initialized)
            {
                return;
            }

            tunerAgcCheckBox.Enabled = samplingModeComboBox.SelectedIndex == 0;
            gainLabel.Visible = tunerAgcCheckBox.Enabled && !tunerAgcCheckBox.Checked;
            tunerGainTrackBar.Enabled = tunerAgcCheckBox.Enabled && !tunerAgcCheckBox.Checked;
            offsetTuningCheckBox.Enabled = samplingModeComboBox.SelectedIndex == 0;

            if (samplingModeComboBox.SelectedIndex == 0)
            {
                offsetTuningCheckBox_CheckedChanged(null, null);
            }
            else
            {
                _owner.Device.UseOffsetTuning = false;
            }
            _owner.Device.SamplingMode = (SamplingMode) samplingModeComboBox.SelectedIndex;
            Utils.SaveSetting("HackRFSamplingMode", samplingModeComboBox.SelectedIndex);
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
            /*
            tunerTypeLabel.Text = _owner.Device.TunerType.ToString();
            */
            tunerGainTrackBar.Maximum = _owner.Device.SupportedGains.Length - 1;
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
            samplerateComboBox_SelectedIndexChanged(null, null);
            samplingModeComboBox_SelectedIndexChanged(null, null);
            offsetTuningCheckBox_CheckedChanged(null, null);
            frequencyCorrectionNumericUpDown_ValueChanged(null, null);
            rtlAgcCheckBox_CheckedChanged(null, null);
            /*
            tunerAgcCheckBox_CheckedChanged(null, null);
            if (!tunerAgcCheckBox.Checked)
            {
                tunerGainTrackBar_Scroll(null, null);
            }
            */
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
            string namestr = "HackRF Device";
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
