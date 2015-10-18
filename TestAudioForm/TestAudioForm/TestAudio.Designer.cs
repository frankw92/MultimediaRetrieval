namespace TestAudioForm
{
    partial class TestAudio
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.startRecordingButton = new System.Windows.Forms.Button();
            this.stopRecordingButton = new System.Windows.Forms.Button();
            this.waveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dbButton = new System.Windows.Forms.Button();
            this.musicButton = new System.Windows.Forms.Button();
            this.dbCheckLabel = new System.Windows.Forms.Label();
            this.emoComboBox = new System.Windows.Forms.ComboBox();
            this.choseLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.waveChart)).BeginInit();
            this.SuspendLayout();
            // 
            // startRecordingButton
            // 
            this.startRecordingButton.Location = new System.Drawing.Point(12, 12);
            this.startRecordingButton.Name = "startRecordingButton";
            this.startRecordingButton.Size = new System.Drawing.Size(722, 23);
            this.startRecordingButton.TabIndex = 0;
            this.startRecordingButton.Text = "Start Recording";
            this.startRecordingButton.UseVisualStyleBackColor = true;
            this.startRecordingButton.Click += new System.EventHandler(this.startRecordingButton_Click);
            // 
            // stopRecordingButton
            // 
            this.stopRecordingButton.Enabled = false;
            this.stopRecordingButton.Location = new System.Drawing.Point(13, 41);
            this.stopRecordingButton.Name = "stopRecordingButton";
            this.stopRecordingButton.Size = new System.Drawing.Size(721, 23);
            this.stopRecordingButton.TabIndex = 1;
            this.stopRecordingButton.Text = "Stop Recording";
            this.stopRecordingButton.UseVisualStyleBackColor = true;
            this.stopRecordingButton.Click += new System.EventHandler(this.stopRecordingButton_Click);
            // 
            // waveChart
            // 
            chartArea1.Name = "ChartArea1";
            this.waveChart.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.waveChart.Legends.Add(legend1);
            this.waveChart.Location = new System.Drawing.Point(13, 71);
            this.waveChart.Name = "waveChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.waveChart.Series.Add(series1);
            this.waveChart.Size = new System.Drawing.Size(721, 533);
            this.waveChart.TabIndex = 2;
            this.waveChart.Text = "waveChart";
            // 
            // dbButton
            // 
            this.dbButton.Location = new System.Drawing.Point(740, 12);
            this.dbButton.Name = "dbButton";
            this.dbButton.Size = new System.Drawing.Size(129, 23);
            this.dbButton.TabIndex = 3;
            this.dbButton.Text = "Create Database";
            this.dbButton.UseVisualStyleBackColor = true;
            this.dbButton.Click += new System.EventHandler(this.dbButton_Click);
            // 
            // musicButton
            // 
            this.musicButton.Location = new System.Drawing.Point(740, 148);
            this.musicButton.Name = "musicButton";
            this.musicButton.Size = new System.Drawing.Size(129, 23);
            this.musicButton.TabIndex = 4;
            this.musicButton.Text = "Play Music";
            this.musicButton.UseVisualStyleBackColor = true;
            this.musicButton.Click += new System.EventHandler(this.musicButton_Click);
            // 
            // dbCheckLabel
            // 
            this.dbCheckLabel.AutoSize = true;
            this.dbCheckLabel.Location = new System.Drawing.Point(740, 42);
            this.dbCheckLabel.Name = "dbCheckLabel";
            this.dbCheckLabel.Size = new System.Drawing.Size(77, 13);
            this.dbCheckLabel.TabIndex = 5;
            this.dbCheckLabel.Text = "Database exist";
            // 
            // emoComboBox
            // 
            this.emoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emoComboBox.FormattingEnabled = true;
            this.emoComboBox.Items.AddRange(new object[] {
            "Happy",
            "Anger",
            "Saddness",
            "Fear",
            "Neutral"});
            this.emoComboBox.Location = new System.Drawing.Point(740, 121);
            this.emoComboBox.Name = "emoComboBox";
            this.emoComboBox.Size = new System.Drawing.Size(121, 21);
            this.emoComboBox.TabIndex = 6;
            // 
            // choseLabel
            // 
            this.choseLabel.AutoSize = true;
            this.choseLabel.Location = new System.Drawing.Point(740, 102);
            this.choseLabel.Name = "choseLabel";
            this.choseLabel.Size = new System.Drawing.Size(126, 13);
            this.choseLabel.TabIndex = 7;
            this.choseLabel.Text = "Chose your goal emotion:";
            // 
            // TestAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 616);
            this.Controls.Add(this.choseLabel);
            this.Controls.Add(this.emoComboBox);
            this.Controls.Add(this.dbCheckLabel);
            this.Controls.Add(this.musicButton);
            this.Controls.Add(this.dbButton);
            this.Controls.Add(this.waveChart);
            this.Controls.Add(this.stopRecordingButton);
            this.Controls.Add(this.startRecordingButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestAudio";
            this.Text = "Sound Analyser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestAudio_FormClosing);
            this.Load += new System.EventHandler(this.TestAudio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.waveChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataVisualization.Charting.Chart waveChart;
        public System.Windows.Forms.Button startRecordingButton;
        public System.Windows.Forms.Button stopRecordingButton;
        private System.Windows.Forms.Button dbButton;
        private System.Windows.Forms.Button musicButton;
        private System.Windows.Forms.Label dbCheckLabel;
        private System.Windows.Forms.ComboBox emoComboBox;
        private System.Windows.Forms.Label choseLabel;
    }
}

