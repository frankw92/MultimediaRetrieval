﻿using System;
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
        private bool paused;
        private WaveOut waveOut;
        private int songnr;

        public MusicPlayer(List<int> songIDs)
        {
            this.songIDs = songIDs;
            this.paused = false;
            this.songnr = -1;
        }

        public int PlaySong()
        {
            this.songnr++;
            if (waveOut != null)
            {
                waveOut.Dispose();
            }
            int songid = songIDs[this.songnr];
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
