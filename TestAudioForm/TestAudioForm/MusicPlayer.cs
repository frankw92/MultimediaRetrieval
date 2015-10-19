using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace TestAudioForm
{
    class MusicPlayer
    {
        private List<int> songIDs;
        private Mp3FileReader mp3Reader;
        private Random r;
        private bool paused;
        private WaveOut waveOut;

        public MusicPlayer(List<int> songIDs)
        {
            this.songIDs = songIDs;
            this.r = new Random();
            this.paused = false;
        }

        public int PlaySong()
        {
            if (waveOut != null)
            {
                waveOut.Dispose();
            }
            int songid = r.Next(1, songIDs.Count + 1);
            string mp3Url = "../../../Music database/clips_45seconds/" + songid.ToString() + ".mp3";
            waveOut = new WaveOut();
            mp3Reader = new Mp3FileReader(mp3Url);
            waveOut.Init(mp3Reader);
            waveOut.Play();
            return songid;
        }

        public void PausePlaySong()
        {
            if (paused)
            {
                paused = false;
                waveOut.Resume();
            }
            else
            {
                paused = true;
                waveOut.Pause();
            }
        }

        public bool Paused
        { 
            get { return this.paused; }
            set { this.paused = value; }
        }
    }
}
