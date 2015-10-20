using System;
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

        public Database()
        {
            dbConnection = new SQLiteConnection("Data Source=db.sqlite;Version=3;");
            CreateDBs();
        }

        public List<int> CreatePlaylist(EmotionVector curEmo, char goalEmo, List<string> prefGenres)
        {
            float startVal, startAr, goalVal, goalAr;

            EmotionVector goalEmotion;
            switch (goalEmo)
            {
                case 'H':
                    goalEmotion = GlobalVariables.Happy;
                    break;
                case 'A':
                    goalEmotion = GlobalVariables.Anger;
                    break;
                case 'S':
                    goalEmotion = GlobalVariables.Sad;
                    break;
                case 'F':
                    goalEmotion = GlobalVariables.Fear;
                    break;
                default:
                    goalEmotion = GlobalVariables.Neutral;
                    break;
            }

            startAr = (float)curEmo.Arousal;
            startVal = (float)curEmo.Valence;
            goalAr = (float)goalEmotion.Arousal;
            goalVal = (float)goalEmotion.Valence;

            float midStartGoalAr = (startAr + goalAr) / 2;
            float midStartGoalVal = (startVal + goalVal) / 2;
            float sumMids = midStartGoalAr + midStartGoalVal;

            string query = "SELECT song_id FROM db WHERE (genre IN (";
            bool first = true;
            foreach (string s in prefGenres)
            {
                if (first)
                {
                    query += "\"" + s + "\"";
                    first = false;
                }
                else
                {
                    query += ", " + "\"" + s + "\"";
                }
            }
            query += ")) ORDER BY (abs(mean_arousal + mean_valence - " + sumMids.ToString().Replace(',','.') + ")) LIMIT 5;";
            dbConnection.Open();
            sql = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader sqlr = sql.ExecuteReader();
            List<int> songIDs = new List<int>();
            while(sqlr.Read())
            {
                songIDs.Add(sqlr.GetInt32(0));
            }
            dbConnection.Close();
            return songIDs;
        }
        
        public void CreateDBs()
        {
            if (File.Exists("db.sqlite"))
                return;

            SQLiteConnection.CreateFile("db.sqlite");
            CreateDB();
        }

        public void CreateDB()
        {
            dbConnection.Open();
            string query = "CREATE TABLE db (";
            reader = new StreamReader(File.OpenRead("../../../Music database/Annotations/db.csv"));
            var line = reader.ReadLine();
            var values = line.Replace("\t", "").Split(';');
            for (int i = 0; i < values.Length; i++)
            {
                if (i == 0)
                    query += values[i] + " INTEGER ";
                else
                    query +=", " + values[i] + " REAL ";
            }

            query += ", PRIMARY KEY (song_id) );";
            sql = new SQLiteCommand(query, dbConnection);
            sql.ExecuteNonQuery();

            while (!reader.EndOfStream)
            {
                query = "INSERT INTO db VALUES (";
                line = reader.ReadLine();
                values = line.Replace("\t", "").Replace(',', '.').Split(';');
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
