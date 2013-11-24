using System;
using System.Globalization;
using System.Windows.Forms;
using SDRSharp.Radio;

namespace SDRSharp.HRFTCP
{
    public partial class HRFTCPSettings : Form
    {
        private const int LNAGainStep = 8;
        private const int VGAGainStep = 2;
        private const int IFFreqOffset = 2400; 
        private readonly HRFTCPIO _owner;

        public string Hostname
        {
            get { return hostBox.Text; }
            set { hostBox.Text = value; }
        }

        public int Port
        {
            get { return (int)portNumberUpDown.Value; }
            set { portNumberUpDown.Value = value; }
        }

        public HRFTCPSettings(HRFTCPIO owner)
        {
            _owner = owner;
            InitializeComponent();
            
            samplerateComboBox.SelectedIndex = Utils.GetIntSetting("HRFTCPSampleRate", 18);
            tunerAmpCheckBox.Checked = Utils.GetBooleanSetting("HRFTCPTunerAmp");
            tunerLNAGainTrackBar.Value = Utils.GetIntSetting("HRFTCPTunerLNAGain", 3);
            tunerVGAGainTrackBar.Value = Utils.GetIntSetting("HRFTCPTunerVGAGain", 12);
            tunerIFFreq.Value = Utils.GetIntSetting("HRFTCPTunerIFFreq", 0);
            gainLNALabel.Text = (tunerLNAGainTrackBar.Value * LNAGainStep) + " dB";
            gainVGALabel.Text = (tunerVGAGainTrackBar.Value * VGAGainStep) + " dB";
            IFLabel.Text = (tunerIFFreq.Value + IFFreqOffset) + " MHz";

            tunerVGAGainTrackBar_Scroll(null, null);
            tunerLNAGainTrackBar_Scroll(null, null);
            tunerIFFreq_Scroll(null, null);
            samplerateComboBox_SelectedIndexChanged(null, null);
            tunerAmpCheckBox_CheckedChanged(null, null);

            UpdateGuiState();
        }
                
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            UpdateGuiState();
        }

        private void UpdateGuiState()
        {
            samplerateComboBox.Enabled = !_owner.IsStreaming;
            hostBox.Enabled = !_owner.IsStreaming;
            portNumberUpDown.Enabled = !_owner.IsStreaming;
        }

        private void HRFTCPSettings_VisibleChanged(object sender, EventArgs e)
        {
            refreshTimer.Enabled = Visible;
            UpdateGuiState();
        }

        private void HRFTCPSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void samplerateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var samplerateString = samplerateComboBox.Items[samplerateComboBox.SelectedIndex].ToString().Split(' ')[0];
            var sampleRate = double.Parse(samplerateString, CultureInfo.InvariantCulture);
            _owner.Samplerate = (uint)(sampleRate * 1000000.0);
            Utils.SaveSetting("HRFTCPSampleRate", samplerateComboBox.SelectedIndex);
        }

        private void tunerAmpCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _owner.UseTunerAMP = tunerAmpCheckBox.Checked;
            Utils.SaveSetting("HRFTCPTunerAmp", tunerAmpCheckBox.Checked);
        }

        private void tunerVGAGainTrackBar_Scroll(object sender, EventArgs e)
        {
            _owner.tunerGainVGA = (tunerVGAGainTrackBar.Value * VGAGainStep);
            gainVGALabel.Text = _owner.tunerGainVGA + " dB";
            Utils.SaveSetting("HRFTCPTunerVGAGain", tunerVGAGainTrackBar.Value);
        }

        private void tunerLNAGainTrackBar_Scroll(object sender, EventArgs e)
        {
            _owner.tunerGainLNA = (tunerLNAGainTrackBar.Value * LNAGainStep);
            gainLNALabel.Text = _owner.tunerGainLNA + " dB";
            Utils.SaveSetting("HRFTCPTunerLNAGain", tunerLNAGainTrackBar.Value);
        }

        private void tunerIFFreq_Scroll(object sender, EventArgs e)
        {
            _owner.tunerIF = (tunerIFFreq.Value + IFFreqOffset);
            IFLabel.Text = _owner.tunerIF + " MHz";
            Utils.SaveSetting("HRFTCPTunerIFFreq", tunerIFFreq.Value);
        }
    }
}
