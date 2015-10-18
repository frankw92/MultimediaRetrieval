﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using NAudio;
using NAudio.Wave;

namespace TestAudioForm
{
    class Database
    {
        SQLiteConnection dbConnection;
        SQLiteCommand sql;
        StreamReader reader;
        Mp3FileReader mp3Reader;

        public Database()
        {
            dbConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
            CreateDBs();
        }

        public void ChangeEmotion(WindowEmotion curEmo, WindowEmotion goalEmo)
        {
            float curVal, curAr, goalVal, goalAr;
            switch (curEmo.Emotion)
            {
                case 'H':
                    curVal = 6.55f;
                    curAr = 6.6f;                    
                    break;
                case 'A':
                    curVal = 3.3f;
                    curAr = 6.6f;
                    break;
                case 'S':
                    curVal = 3.3f;
                    curAr = 3.2f;
                    break;
                case 'F':
                    curVal = 6.55f;
                    curAr = 3.2f;
                    break;
                default:
                    curVal = 5.0f;
                    curAr = 4.8f;
                    break;
            }
            switch (goalEmo.Emotion)
            {
                case 'H':
                    goalAr = 8.4f;
                    goalVal = 8.1f;
                    break;
                case 'A':
                    goalAr = 8.4f;
                    goalVal = 1.6f;
                    break;
                case 'S':
                    goalAr = 1.6f;
                    goalVal = 1.6f;
                    break;
                case 'F':
                    goalAr = 1.6f;
                    goalVal = 8.1f;
                    break;
                default:
                    goalAr = 4.8f;
                    goalVal = 5.0f;
                    break;
            }
            string query = "SELECT song_id FROM static_annotations WHERE (mean_arousal BETWEEN ";
            if (curAr > goalAr)
            {
                if (curVal > goalVal)
                {
                    query += goalAr.ToString().Replace(',', '.') + " AND " + curAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + goalVal.ToString().Replace(',', '.') + " AND " + curVal.ToString().Replace(',', '.') + ");";
                }
                else if (curVal < goalVal)
                {
                    query += goalAr.ToString().Replace(',', '.') + " AND " + curAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + curVal.ToString().Replace(',', '.') + " AND " + goalVal.ToString().Replace(',', '.') + ");";
                }
                else
                {
                    query += goalAr.ToString().Replace(',', '.') + " AND " + curAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + (goalVal - 1.0f).ToString().Replace(',', '.') + " AND " + (curVal + 1.0f).ToString().Replace(',', '.') + ");";
                }
            }
            else if (curAr < goalAr)
            {
                if (curVal > goalVal)
                {
                    query += curAr.ToString().Replace(',', '.') + " AND " + goalAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + goalVal.ToString().Replace(',', '.') + " AND " + curVal.ToString().Replace(',', '.') + ");";
                }
                else if (curVal < goalVal)
                {
                    query += curAr.ToString().Replace(',', '.') + " AND " + goalAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + curVal.ToString().Replace(',', '.') + " AND " + goalVal.ToString().Replace(',', '.') + ");";
                }
                else
                {
                    query += goalAr.ToString().Replace(',', '.') + " AND " + curAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + (goalVal - 1.0f).ToString().Replace(',', '.') + " AND " + (curVal + 1.0f).ToString().Replace(',', '.') + ");";
                }
            }
            else
            {
                if (curVal > goalVal)
                {
                    query += (goalAr - 1.0f).ToString().Replace(',', '.') + " AND " + (curAr + 1.0f).ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + goalVal.ToString().Replace(',', '.') + " AND " + curVal.ToString().Replace(',', '.') + ");";
                }
                else if (curVal < goalVal)
                {
                    query += (goalAr - 1.0f).ToString().Replace(',', '.') + " AND " + (curAr + 1.0f).ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + curVal.ToString().Replace(',', '.') + " AND " + goalVal.ToString().Replace(',', '.') + ");";
                }
                else
                {
                    query += (goalAr - 1.0f).ToString().Replace(',', '.') + " AND " + (curAr + 1.0f).ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + (goalVal - 1.0f).ToString().Replace(',', '.') + " AND " + (curVal + 1.0f).ToString().Replace(',', '.') + ");";
                }
            }
            dbConnection.Open();
            sql = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader sqlr = sql.ExecuteReader();
            List<int> songIDs = new List<int>();
            while(sqlr.Read())
            {
                songIDs.Add(sqlr.GetInt32(0));
            }
            dbConnection.Close();
            Random r = new Random();
            PlaySong(songIDs[r.Next(songIDs.Count + 1)]);
        }

        public void PlaySong(int songid)
        {
            string mp3Url = "../../../Music database/clips_45seconds/" + songid.ToString() + ".mp3";
            mp3Reader = new Mp3FileReader(mp3Url);
            var waveOut = new WaveOut(); 
            waveOut.Init(mp3Reader);
            waveOut.Play();
        }

        public void CreateDBs()
        {
            if (File.Exists("db.sqlite"))
                return;

            SQLiteConnection.CreateFile("db.sqlite");
            CreateDB("../../../Music database/Annotations/static_annotations.csv", "static_annotations");
        }

        private void CreateDB(string path, string name)
        {
            dbConnection.Open();
            string query = "CREATE TABLE  "+ name +" (";
            reader = new StreamReader(File.OpenRead(path));
            var line = reader.ReadLine();
            var values = line.Split(';');

            for (int i = 0; i < values.Length; i++)
            {
                if (i == 0)
                    query += values[i] + " integer, ";
                else
                    query += values[i] + " real, ";
            }

            query += "PRIMARY KEY (song_id) );";
            sql = new SQLiteCommand(query, dbConnection);
            sql.ExecuteNonQuery();

            while (!reader.EndOfStream)
            {
                query = "INSERT INTO " + name + " VALUES (";
                line = reader.ReadLine();
                values = line.Split(';');
                for (int i = 0; i < values.Length; i++)
                {
                    if (i == 0)
                        query += values[i];
                    else
                        query += ", " + values[i];
                    
                }
                query += ");";
                sql = new SQLiteCommand(query, dbConnection);
                sql.ExecuteNonQuery();
            }

            dbConnection.Close();
        }
    }
}
