using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizNoUI.MVVM.Model
{
    class AllQuizzesModel
    {
        public int CurrentQuiz { get; set; }
        public int CurrentQuestion { get; set; }
        public bool GameScore { get; set; }
        public bool GameStarted { get; set; }
        public ObservableCollection<QuizModel> QuizModels { get; set; }

        public void LoadQuizzes()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=C:\\Users\\rafal\\source\\repos\\QuizNoUI\\QuizNoUI\\QuizzessDatabase.db"))
            { 
                connection.Close();
            }
}
}
}
