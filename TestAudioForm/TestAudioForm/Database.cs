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

        public List<int> CreatePlaylist(char curEmo, char goalEmo, List<string> prefGenres)
        {
            float startVal, startAr, goalVal, goalAr;
            float meanVal = 5.0f, meanAr = 4.8f;
            switch (goalEmo)
            {
                case 'H':
                    goalAr = 8.4f;
                    goalVal = 8.1f;
                    switch(curEmo)
                    {
                        case 'H':
                            startVal = 6.55f;
                            startAr = 6.6f;
                            break;
                        case 'A':
                            startVal = meanVal;
                            startAr = 6.6f;
                            break;
                        case 'F':
                            startVal = 6.55f;
                            startAr = meanAr;
                            break;
                        default:
                            startVal = meanVal;
                            startAr = meanAr;
                            break;
                    }
                    break;
                case 'A':
                    goalAr = 8.4f;
                    goalVal = 1.6f;
                    switch (curEmo)
                    {
                        case 'H':
                            startVal = meanVal;
                            startAr = 6.6f;
                            break;
                        case 'A':
                            startVal = 3.3f;
                            startAr = 6.6f;
                            break;
                        case 'S':
                            startVal = 3.3f;
                            startAr = meanAr;
                            break;
                        default:
                            startVal = meanVal;
                            startAr = meanAr;
                            break;
                    }
                    break;
                case 'S':
                    goalAr = 1.6f;
                    goalVal = 1.6f;
                    switch (curEmo)
                    {
                        case 'A':
                            startVal = 3.3f;
                            startAr = meanAr;
                            break;
                        case 'S':
                            startVal = 3.3f;
                            startAr = 3.2f;
                            break;
                        case 'F':
                            startVal = meanVal;
                            startAr = 3.2f;
                            break;
                        default:
                            startVal = meanVal;
                            startAr = meanAr;
                            break;
                    }
                    break;
                case 'F':
                    goalAr = 1.6f;
                    goalVal = 8.1f;
                    switch (curEmo)
                    {
                        case 'H':
                            startVal = 6.55f;
                            startAr = meanAr;
                            break;
                        case 'S':
                            startVal = meanVal;
                            startAr = 3.2f;
                            break;
                        case 'F':
                            startVal = 6.55f;
                            startAr = 3.2f;
                            break;
                        default:
                            startVal = meanVal;
                            startAr = meanAr;
                            break;
                    }
                    break;
                default:
                    goalAr = meanAr;
                    goalVal = meanVal;
                    switch (curEmo)
                    {
                        case 'H':
                            startVal = 6.55f;
                            startAr = 6.6f;
                            break;
                        case 'A':
                            startVal = 3.3f;
                            startAr = 6.6f;
                            break;
                        case 'S':
                            startVal = 3.3f;
                            startAr = 3.2f;
                            break;
                        case 'F':
                            startVal = 6.55f;
                            startAr = 3.2f;
                            break;
                        default:
                            startVal = meanVal;
                            startAr = meanAr;
                            break;
                    }
                    break;
            }
            string query = "SELECT song_id FROM db WHERE (mean_arousal BETWEEN ";
            if (startAr > goalAr)
            {
                if (startVal > goalVal)
                {
                    query += goalAr.ToString().Replace(',', '.') + " AND " + startAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + goalVal.ToString().Replace(',', '.') + " AND " + startVal.ToString().Replace(',', '.') + ")";
                }
                else if (startVal < goalVal)
                {
                    query += goalAr.ToString().Replace(',', '.') + " AND " + startAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + startVal.ToString().Replace(',', '.') + " AND " + goalVal.ToString().Replace(',', '.') + ")";
                }
                else
                {
                    query += goalAr.ToString().Replace(',', '.') + " AND " + startAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + (goalVal - 1.0f).ToString().Replace(',', '.') + " AND " + (startVal + 1.0f).ToString().Replace(',', '.') + ")";
                }
            }
            else if (startAr < goalAr)
            {
                if (startVal > goalVal)
                {
                    query += startAr.ToString().Replace(',', '.') + " AND " + goalAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + goalVal.ToString().Replace(',', '.') + " AND " + startVal.ToString().Replace(',', '.') + ")";
                }
                else if (startVal < goalVal)
                {
                    query += startAr.ToString().Replace(',', '.') + " AND " + goalAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + startVal.ToString().Replace(',', '.') + " AND " + goalVal.ToString().Replace(',', '.') + ")";
                }
                else
                {
                    query += goalAr.ToString().Replace(',', '.') + " AND " + startAr.ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + (goalVal - 1.0f).ToString().Replace(',', '.') + " AND " + (startVal + 1.0f).ToString().Replace(',', '.') + ")";
                }
            }
            else
            {
                if (startVal > goalVal)
                {
                    query += (goalAr - 1.0f).ToString().Replace(',', '.') + " AND " + (startAr + 1.0f).ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + goalVal.ToString().Replace(',', '.') + " AND " + startVal.ToString().Replace(',', '.') + ")";
                }
                else if (startVal < goalVal)
                {
                    query += (goalAr - 1.0f).ToString().Replace(',', '.') + " AND " + (startAr + 1.0f).ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + startVal.ToString().Replace(',', '.') + " AND " + goalVal.ToString().Replace(',', '.') + ")";
                }
                else
                {
                    query += (goalAr - 1.0f).ToString().Replace(',', '.') + " AND " + (startAr + 1.0f).ToString().Replace(',', '.') + ") AND (mean_valence BETWEEN " + (goalVal - 1.0f).ToString().Replace(',', '.') + " AND " + (startVal + 1.0f).ToString().Replace(',', '.') + ")";
                }
            }

            query += " AND (genre IN (";
            bool first = true;
            foreach(string s in prefGenres)
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
            query += "));";

            dbConnection.Open();
            sql = new SQLiteCommand(query, dbConnection);
            SQLiteDataReader sqlr = sql.ExecuteReader();
            List<int> songIDs = new List<int>();
            while(sqlr.Read())
            {
                songIDs.Add(sqlr.GetInt32(0));
            }
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
