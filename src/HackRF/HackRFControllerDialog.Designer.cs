namespace SDRSharp.HackRF
{
    partial class HackRFControllerDialog
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
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.closeButton = new System.Windows.Forms.Button();
            this.deviceComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tunerLNAGainTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.samplerateComboBox = new System.Windows.Forms.ComboBox();
            this.tunerAmpCheckBox = new System.Windows.Forms.CheckBox();
            this.gainLNALabel = new System.Windows.Forms.Label();
            this.tunerTypeLabel = new System.Windows.Forms.Label();
            this.gainVGALabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tunerVGAGainTrackBar = new System.Windows.Forms.TrackBar();
            this.IFLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tunerIFFreq = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.tunerLNAGainTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerVGAGainTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerIFFreq)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(184, 282);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deviceComboBox.FormattingEnabled = true;
            this.deviceComboBox.Location = new System.Drawing.Point(12, 26);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Size = new System.Drawing.Size(247, 21);
            this.deviceComboBox.TabIndex = 0;
            this.deviceComboBox.SelectedIndexChanged += new System.EventHandler(this.deviceComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Device";
            // 
            // tunerLNAGainTrackBar
            // 
            this.tunerLNAGainTrackBar.Location = new System.Drawing.Point(3, 135);
            this.tunerLNAGainTrackBar.Maximum = 5;
            this.tunerLNAGainTrackBar.Name = "tunerLNAGainTrackBar";
            this.tunerLNAGainTrackBar.Size = new System.Drawing.Size(267, 45);
            this.tunerLNAGainTrackBar.TabIndex = 6;
            this.tunerLNAGainTrackBar.Scroll += new System.EventHandler(this.tunerLNAGainTrackBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "LNA Gain";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Sample Rate";
            // 
            // samplerateComboBox
            // 
            this.samplerateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.samplerateComboBox.FormattingEnabled = true;
            this.samplerateComboBox.Items.AddRange(new object[] {
            "20 MSPS     (Slight Sample Loss)",
            "16 MSPS     ",
            "12.5 MSPS  ",
            "10 MSPS     ",
            "8 MSPS       (Zefie Recommended)",
            "4 MSPS       ",
            "3.2 MSPS    (Slower PCs)",
            "2 MSPS       (RTLSDR Compatible)",
            "1 MSPS       (RTLSDR Compatible)"});
            this.samplerateComboBox.Location = new System.Drawing.Point(12, 70);
            this.samplerateComboBox.Name = "samplerateComboBox";
            this.samplerateComboBox.Size = new System.Drawing.Size(247, 21);
            this.samplerateComboBox.TabIndex = 1;
            this.samplerateComboBox.SelectedIndexChanged += new System.EventHandler(this.samplerateComboBox_SelectedIndexChanged);
            // 
            // tunerAmpCheckBox
            // 
            this.tunerAmpCheckBox.AutoSize = true;
            this.tunerAmpCheckBox.Checked = true;
            this.tunerAmpCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tunerAmpCheckBox.Enabled = false;
            this.tunerAmpCheckBox.Location = new System.Drawing.Point(12, 99);
            this.tunerAmpCheckBox.Name = "tunerAmpCheckBox";
            this.tunerAmpCheckBox.Size = new System.Drawing.Size(166, 17);
            this.tunerAmpCheckBox.TabIndex = 5;
            this.tunerAmpCheckBox.Text = "Enable HackRF Internal AMP";
            this.tunerAmpCheckBox.UseVisualStyleBackColor = true;
            this.tunerAmpCheckBox.CheckedChanged += new System.EventHandler(this.tunerAmpCheckBox_CheckedChanged);
            // 
            // gainLNALabel
            // 
            this.gainLNALabel.Location = new System.Drawing.Point(191, 119);
            this.gainLNALabel.Name = "gainLNALabel";
            this.gainLNALabel.Size = new System.Drawing.Size(68, 13);
            this.gainLNALabel.TabIndex = 26;
            this.gainLNALabel.Text = "1000dB";
            this.gainLNALabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.gainLNALabel.Visible = false;
            // 
            // tunerTypeLabel
            // 
            this.tunerTypeLabel.Location = new System.Drawing.Point(166, 9);
            this.tunerTypeLabel.Name = "tunerTypeLabel";
            this.tunerTypeLabel.Size = new System.Drawing.Size(93, 13);
            this.tunerTypeLabel.TabIndex = 29;
            this.tunerTypeLabel.Text = "HackRF";
            this.tunerTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gainVGALabel
            // 
            this.gainVGALabel.Location = new System.Drawing.Point(191, 170);
            this.gainVGALabel.Name = "gainVGALabel";
            this.gainVGALabel.Size = new System.Drawing.Size(68, 13);
            this.gainVGALabel.TabIndex = 34;
            this.gainVGALabel.Text = "1000dB";
            this.gainVGALabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.gainVGALabel.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "VGA Gain";
            // 
            // tunerVGAGainTrackBar
            // 
            this.tunerVGAGainTrackBar.Location = new System.Drawing.Point(3, 186);
            this.tunerVGAGainTrackBar.Maximum = 31;
            this.tunerVGAGainTrackBar.Name = "tunerVGAGainTrackBar";
            this.tunerVGAGainTrackBar.Size = new System.Drawing.Size(267, 45);
            this.tunerVGAGainTrackBar.TabIndex = 32;
            this.tunerVGAGainTrackBar.Scroll += new System.EventHandler(this.tunerVGAGainTrackBar_Scroll);
            // 
            // IFLabel
            // 
            this.IFLabel.Location = new System.Drawing.Point(191, 221);
            this.IFLabel.Name = "IFLabel";
            this.IFLabel.Size = new System.Drawing.Size(68, 13);
            this.IFLabel.TabIndex = 38;
            this.IFLabel.Text = "2400MHz";
            this.IFLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.IFLabel.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Intermediate Frequency";
            // 
            // tunerIFFreq
            // 
            this.tunerIFFreq.Location = new System.Drawing.Point(3, 237);
            this.tunerIFFreq.Maximum = 300;
            this.tunerIFFreq.Name = "tunerIFFreq";
            this.tunerIFFreq.Size = new System.Drawing.Size(267, 45);
            this.tunerIFFreq.TabIndex = 36;
            this.tunerIFFreq.Scroll += new System.EventHandler(this.tunerIFFreq_Scroll);
            // 
            // HackRFControllerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(271, 315);
            this.Controls.Add(this.IFLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tunerIFFreq);
            this.Controls.Add(this.gainVGALabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tunerVGAGainTrackBar);
            this.Controls.Add(this.tunerTypeLabel);
            this.Controls.Add(this.gainLNALabel);
            this.Controls.Add(this.tunerAmpCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.samplerateComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tunerLNAGainTrackBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deviceComboBox);
            this.Controls.Add(this.closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HackRFControllerDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HackRF Controller";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HackRFControllerDialog_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.HackRFControllerDialog_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.tunerLNAGainTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerVGAGainTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunerIFFreq)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ComboBox deviceComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar tunerLNAGainTrackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox samplerateComboBox;
        private System.Windows.Forms.CheckBox tunerAmpCheckBox;
        private System.Windows.Forms.Label gainLNALabel;
        private System.Windows.Forms.Label tunerTypeLabel;
        private System.Windows.Forms.Label gainVGALabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar tunerVGAGainTrackBar;
        private System.Windows.Forms.Label IFLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tunerIFFreq;
    }
}

