using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizNoUI.MVVM.Model
{
    class QuizModel
    {
        public List<int> QuestionsIds = new List<int>();
        public QuizModel(int quizid)
        {
            DatabaseAcces database = new DatabaseAcces();
            DataTable dt = new DataTable();
            dt = database.dbConnect($"select pytanie_id from Pytanie where quiz_id={quizid};");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["pytanie_id"]);
                QuestionsIds.Add(id);
            }


        }
    }
}
