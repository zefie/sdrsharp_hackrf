namespace SDRSharp.HRFTCP
{
    partial class HRFTCPSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.hostBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tunerAmpCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.samplerateComboBox = new System.Windows.Forms.ComboBox();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.portNumberUpDown = new System.Windows.Forms.NumericUpDown();
            this.IFLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tunerIFFreq = new System.Windows.Forms.TrackBar();
            this.gainVGALabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tunerVGAGainTrackBar = new System.Windows.Forms.TrackBar();
            this.gainLNALabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tunerLNAGainTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.portNumberUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerIFFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerVGAGainTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerLNAGainTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // hostBox
            // 
            this.hostBox.Location = new System.Drawing.Point(119, 12);
            this.hostBox.Name = "hostBox";
            this.hostBox.Size = new System.Drawing.Size(138, 20);
            this.hostBox.TabIndex = 0;
            this.hostBox.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port";
            // 
            // tunerAmpCheckBox
            // 
            this.tunerAmpCheckBox.AutoSize = true;
            this.tunerAmpCheckBox.Checked = true;
            this.tunerAmpCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tunerAmpCheckBox.Location = new System.Drawing.Point(10, 111);
            this.tunerAmpCheckBox.Name = "tunerAmpCheckBox";
            this.tunerAmpCheckBox.Size = new System.Drawing.Size(78, 17);
            this.tunerAmpCheckBox.TabIndex = 5;
            this.tunerAmpCheckBox.Text = "Tuner Amp";
            this.tunerAmpCheckBox.UseVisualStyleBackColor = true;
            this.tunerAmpCheckBox.CheckedChanged += new System.EventHandler(this.tunerAmpCheckBox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Sample Rate";
            // 
            // samplerateComboBox
            // 
            this.samplerateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.samplerateComboBox.FormattingEnabled = true;
            this.samplerateComboBox.Items.AddRange(new object[] {
            "20.0 MSPS     (334mbps, USB upper limit)",
            "19.5 MSPS     (325mbps)",
            "19.0 MSPS     (317mbps)",
            "18.5 MSPS     (308mbps)",
            "18.0 MSPS     (300mbps)",
            "17.5 MSPS     (292mbps)",
            "17.0 MSPS     (284mbps)",
            "16.0 MSPS     (267mbps)",
            "14.0 MSPS     (234mbps)",
            "12.0 MSPS     (200mbps)",
            "10.0 MSPS     (167mbps)",
            "8.0 MSPS       (134mbps)",
            "6.0 MSPS       (100mbps)",
            "5.8 MSPS       ( 97mbps)",
            "5.0 MSPS       ( 84mbps)",
            "4.0 MSPS       ( 67mbps)",
            "3.2 MSPS       ( 54mbps)",
            "2.8 MSPS       ( 47mbps)",
            "2.4 MSPS       ( 40mbps)",
            "2.048 MSPS   ( 34mbps)",
            "1.92 MSPS     ( 32mbps)",
            "1.8 MSPS       ( 30mbps)",
            "1.4 MSPS       ( 24mbps)",
            "0.6 MSPS       ( 10mbps)",
            "0.3 MSPS       (   5mbps)"});
            this.samplerateComboBox.Location = new System.Drawing.Point(10, 84);
            this.samplerateComboBox.Name = "samplerateComboBox";
            this.samplerateComboBox.Size = new System.Drawing.Size(247, 21);
            this.samplerateComboBox.TabIndex = 3;
            this.samplerateComboBox.SelectedIndexChanged += new System.EventHandler(this.samplerateComboBox_SelectedIndexChanged);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // portNumberUpDown
            // 
            this.portNumberUpDown.Location = new System.Drawing.Point(119, 38);
            this.portNumberUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumberUpDown.Name = "portNumberUpDown";
            this.portNumberUpDown.Size = new System.Drawing.Size(138, 20);
            this.portNumberUpDown.TabIndex = 2;
            this.portNumberUpDown.Value = new decimal(new int[] {
            1234,
            0,
            0,
            0});
            // 
            // IFLabel
            // 
            this.IFLabel.Location = new System.Drawing.Point(188, 232);
            this.IFLabel.Name = "IFLabel";
            this.IFLabel.Size = new System.Drawing.Size(68, 14);
            this.IFLabel.TabIndex = 47;
            this.IFLabel.Text = "2400MHz";
            this.IFLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Intermediate Frequency";
            // 
            // tunerIFFreq
            // 
            this.tunerIFFreq.Location = new System.Drawing.Point(0, 248);
            this.tunerIFFreq.Maximum = 300;
            this.tunerIFFreq.Name = "tunerIFFreq";
            this.tunerIFFreq.Size = new System.Drawing.Size(267, 45);
            this.tunerIFFreq.TabIndex = 45;
            this.tunerIFFreq.Scroll += new System.EventHandler(this.tunerIFFreq_Scroll);
            // 
            // gainVGALabel
            // 
            this.gainVGALabel.Location = new System.Drawing.Point(188, 181);
            this.gainVGALabel.Name = "gainVGALabel";
            this.gainVGALabel.Size = new System.Drawing.Size(68, 14);
            this.gainVGALabel.TabIndex = 44;
            this.gainVGALabel.Text = "1000dB";
            this.gainVGALabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "VGA Gain";
            // 
            // tunerVGAGainTrackBar
            // 
            this.tunerVGAGainTrackBar.Location = new System.Drawing.Point(0, 197);
            this.tunerVGAGainTrackBar.Maximum = 31;
            this.tunerVGAGainTrackBar.Name = "tunerVGAGainTrackBar";
            this.tunerVGAGainTrackBar.Size = new System.Drawing.Size(267, 45);
            this.tunerVGAGainTrackBar.TabIndex = 42;
            this.tunerVGAGainTrackBar.Scroll += new System.EventHandler(this.tunerVGAGainTrackBar_Scroll);
            // 
            // gainLNALabel
            // 
            this.gainLNALabel.Location = new System.Drawing.Point(188, 130);
            this.gainLNALabel.Name = "gainLNALabel";
            this.gainLNALabel.Size = new System.Drawing.Size(68, 14);
            this.gainLNALabel.TabIndex = 41;
            this.gainLNALabel.Text = "1000dB";
            this.gainLNALabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "LNA Gain";
            // 
            // tunerLNAGainTrackBar
            // 
            this.tunerLNAGainTrackBar.Location = new System.Drawing.Point(0, 146);
            this.tunerLNAGainTrackBar.Maximum = 5;
            this.tunerLNAGainTrackBar.Name = "tunerLNAGainTrackBar";
            this.tunerLNAGainTrackBar.Size = new System.Drawing.Size(267, 45);
            this.tunerLNAGainTrackBar.TabIndex = 39;
            this.tunerLNAGainTrackBar.Scroll += new System.EventHandler(this.tunerLNAGainTrackBar_Scroll);
            // 
            // HRFTCPSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 288);
            this.Controls.Add(this.IFLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tunerIFFreq);
            this.Controls.Add(this.gainVGALabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tunerVGAGainTrackBar);
            this.Controls.Add(this.gainLNALabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tunerLNAGainTrackBar);
            this.Controls.Add(this.portNumberUpDown);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.samplerateComboBox);
            this.Controls.Add(this.tunerAmpCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hostBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HRFTCPSettings";
            this.ShowInTaskbar = false;
            this.Text = "HackRF-TCP Settings";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HRFTCPSettings_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.HRFTCPSettings_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.portNumberUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerIFFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerVGAGainTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerLNAGainTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hostBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox tunerAmpCheckBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox samplerateComboBox;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.NumericUpDown portNumberUpDown;
        private System.Windows.Forms.Label IFLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tunerIFFreq;
        private System.Windows.Forms.Label gainVGALabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar tunerVGAGainTrackBar;
        private System.Windows.Forms.Label gainLNALabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tunerLNAGainTrackBar;
    }
}