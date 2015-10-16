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
        }

        public void ChangeEmotion(Emotion curEmo, Emotion goalEmo)
        {

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
