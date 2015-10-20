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
            this.musicButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.waveChart)).BeginInit();
            this.SuspendLayout();
            // 
            // startRecordingButton
            // 
            this.startRecordingButton.Location = new System.Drawing.Point(16, 15);
            this.startRecordingButton.Margin = new System.Windows.Forms.Padding(4);
            this.startRecordingButton.Name = "startRecordingButton";
            this.startRecordingButton.Size = new System.Drawing.Size(493, 23);
            this.startRecordingButton.TabIndex = 0;
            this.startRecordingButton.Text = "Start Recording";
            this.startRecordingButton.UseVisualStyleBackColor = true;
            this.startRecordingButton.Click += new System.EventHandler(this.startRecordingButton_Click);
            // 
            // stopRecordingButton
            // 
            this.stopRecordingButton.Enabled = false;
            this.stopRecordingButton.Location = new System.Drawing.Point(17, 50);
            this.stopRecordingButton.Margin = new System.Windows.Forms.Padding(4);
            this.stopRecordingButton.Name = "stopRecordingButton";
            this.stopRecordingButton.Size = new System.Drawing.Size(492, 23);
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
            this.waveChart.Location = new System.Drawing.Point(17, 87);
            this.waveChart.Margin = new System.Windows.Forms.Padding(4);
            this.waveChart.Name = "waveChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.waveChart.Series.Add(series1);
            this.waveChart.Size = new System.Drawing.Size(628, 473);
            this.waveChart.TabIndex = 2;
            this.waveChart.Text = "waveChart";
            // 
            // musicButton
            // 
            this.musicButton.Location = new System.Drawing.Point(537, 13);
            this.musicButton.Margin = new System.Windows.Forms.Padding(4);
            this.musicButton.Name = "musicButton";
            this.musicButton.Size = new System.Drawing.Size(108, 23);
            this.musicButton.TabIndex = 4;
            this.musicButton.Text = "Play Next Song";
            this.musicButton.UseVisualStyleBackColor = true;
            this.musicButton.Click += new System.EventHandler(this.musicButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(537, 50);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(108, 23);
            this.pauseButton.TabIndex = 5;
            this.pauseButton.Text = "Pause/Play Music";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // TestAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 580);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.musicButton);
            this.Controls.Add(this.waveChart);
            this.Controls.Add(this.stopRecordingButton);
            this.Controls.Add(this.startRecordingButton);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Button musicButton;
        private System.Windows.Forms.Button pauseButton;
    }
}

