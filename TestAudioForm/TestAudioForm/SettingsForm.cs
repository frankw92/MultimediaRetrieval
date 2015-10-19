using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAudioForm
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void continueBtn_Click(object sender, EventArgs e)
        {
            errorLbl.Hide();

            // gather all user settings
            char gender = GetGender();
            List<string> genres = GetGenrePreferences();
            char goal = GetGoalEmotion();

            // check if all settings are sets
            if (gender == 'n' || genres.Count == 0 || goal == 'X')
            {
                errorLbl.Show();
                return;
            }

            // create audio form and pass on user settings
            TestAudio audioForm = new TestAudio(new UserSettings(gender, genres, goal));
            audioForm.FormClosed += audioForm_FormClosed;
            audioForm.Show();

            // hide current form, don't close
            this.Hide();
        }

        void audioForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private char GetGender()
        {
            if (maleRb.Checked)
                return 'm';
            if (femaleRb.Checked)
                return 'f';
            return 'n';
        }

        private List<string> GetGenrePreferences()
        {
            List<string> genres = new List<string>();
            foreach (Control c in genreGb.Controls)
                if (c is CheckBox && (c as CheckBox).Checked)
                    genres.Add(c.Text);
            return genres;
        }

        private char GetGoalEmotion()
        {
            foreach (Control c in goalGb.Controls)
                if (c is RadioButton && (c as RadioButton).Checked)
                    return Convert.ToChar(c.Tag);

            return 'X';
        }
    }
}
