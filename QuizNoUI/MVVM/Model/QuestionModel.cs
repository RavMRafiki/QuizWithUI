using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizNoUI.MVVM.Model
{
    public class QuestionModel
    {
        public string QuestionText { get; set; }
        public List<AnswerModel> answers = new List<AnswerModel>();

        public QuestionModel(int pytanieid)
        {
            DatabaseAcces database = new DatabaseAcces();
            DataTable dt = new DataTable();
            dt = database.dbConnect($"select tresc_pytania from Pytanie where pytanie_id={pytanieid};");
            QuestionText = Convert.ToString(dt.Rows[0]["tresc_pytania"]);

            dt = database.dbConnect($"select tresc_odpowiedzi, czy_poprawna from Odpowiedz where pytanie_id={pytanieid};");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AnswerModel answerModel = new AnswerModel(Convert.ToString(dt.Rows[i]["tresc_odpowiedzi"]), Convert.ToBoolean(dt.Rows[i]["czy_poprawna"]));
                answers.Add(answerModel);
            }
             
        }
    }
}
