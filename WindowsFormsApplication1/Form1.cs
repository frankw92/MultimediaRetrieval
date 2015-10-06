using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Audio;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Microphone mic = Microphone.Default;
            if (mic == null)
            {
                throw new Exception("no mic attached");
            }

            byte[] buffer = new byte[Microphone.Default.GetSampleSizeInBytes(TimeSpan.FromSeconds(3.0))];

            int bytesRead = 0;

            while (true)
            {
                Microphone.Default.Start();

                bytesRead += Microphone.Default.GetData(buffer, bytesRead, (buffer.Length - bytesRead));
            }
        }
    }
}
