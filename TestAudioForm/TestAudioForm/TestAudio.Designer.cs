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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.startRecordingButton = new System.Windows.Forms.Button();
            this.stopRecordingButton = new System.Windows.Forms.Button();
            this.waveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dbButton = new System.Windows.Forms.Button();
            this.musicButton = new System.Windows.Forms.Button();
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
            chartArea2.Name = "ChartArea1";
            this.waveChart.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.waveChart.Legends.Add(legend2);
            this.waveChart.Location = new System.Drawing.Point(13, 71);
            this.waveChart.Name = "waveChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.waveChart.Series.Add(series2);
            this.waveChart.Size = new System.Drawing.Size(721, 533);
            this.waveChart.TabIndex = 2;
            this.waveChart.Text = "waveChart";
            // 
            // dbButton
            // 
            this.dbButton.Location = new System.Drawing.Point(13, 580);
            this.dbButton.Name = "dbButton";
            this.dbButton.Size = new System.Drawing.Size(129, 23);
            this.dbButton.TabIndex = 3;
            this.dbButton.Text = "Create Database";
            this.dbButton.UseVisualStyleBackColor = true;
            this.dbButton.Click += new System.EventHandler(this.dbButton_Click);
            // 
            // musicButton
            // 
            this.musicButton.Location = new System.Drawing.Point(149, 580);
            this.musicButton.Name = "musicButton";
            this.musicButton.Size = new System.Drawing.Size(152, 23);
            this.musicButton.TabIndex = 4;
            this.musicButton.Text = "Play Music";
            this.musicButton.UseVisualStyleBackColor = true;
            this.musicButton.Click += new System.EventHandler(this.musicButton_Click);
            // 
            // TestAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 616);
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
            ((System.ComponentModel.ISupportInitialize)(this.waveChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataVisualization.Charting.Chart waveChart;
        public System.Windows.Forms.Button startRecordingButton;
        public System.Windows.Forms.Button stopRecordingButton;
        private System.Windows.Forms.Button dbButton;
        private System.Windows.Forms.Button musicButton;
    }
}

