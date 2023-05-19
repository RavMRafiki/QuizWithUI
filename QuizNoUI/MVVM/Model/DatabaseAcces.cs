using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizNoUI.MVVM.Model
{
    class DatabaseAcces
    {
        public List<int> _questions = new List<int>();
        public List<int> _quizzes = new List<int>();
        public DataTable dbConnect(string command)
        {
            using (SQLiteConnection sqlite = new SQLiteConnection("Data Source=QuizzessDatabase.db; Version=3;"))
            {
                SQLiteDataAdapter ad;
                DataTable dt = new DataTable();
                try
                {
                    SQLiteCommand cmd;
                    sqlite.Open();
                    cmd = sqlite.CreateCommand();
                    cmd.CommandText = command;
                    ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(dt);
                }
                catch (SQLiteException ex) 
                { 
                    Console.WriteLine(ex.Message);
                }
                sqlite.Close();
                return dt;
            }
        }
        public int[] readDatabase(int index = 0)
        {
            DataTable dt = new DataTable();
            dt = dbConnect("select distinct quiz_id from pytanie;");
            _questions.Clear();
            _quizzes.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _quizzes.Add(Convert.ToInt32(dt.Rows[i]["quiz_id"]));
            }
            _quizzes.Sort();
            //MessageBox.Show(string.Join(", ", _quizzes));
            int[] ints = _quizzes.ToArray();
            return ints;
        }
        public string quizName(int id)
        {
            DataTable dt = new DataTable();
            dt = dbConnect($"select nazwa_quizu from Quiz where quiz_id={id};");
            string nazwa = Convert.ToString(dt.Rows[0]["nazwa_quizu"]);
            //MessageBox.Show(nazwa);
            return nazwa;
        }

    }
}
