using Dapper;
using QuizNoUI.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizNoUI.MVVM.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Model.AllQuizzesModel allQuizzesModel = new Model.AllQuizzesModel();

        private const string ConnectionString = @"Data Source=QuizzessDatabase.db";


        public int CurrentQuiz { get; set; }
        public string CurrentQuestionText { get; set; }
        public int CurrentQuestion { get; set; }
        public int GameScore { get; set; }
        public bool GameStarted { get; set; }
        public bool GameFinished { get; set; }
        public string[] AnswersText { get; set; }
        public bool[] Answers { get; set; }
        public List<QuizModel> Quizzes { get; set; }
        private List<QuizModel> QuizModels()
        {
            using (IDbConnection cnn = new SQLiteConnection(ConnectionString))
            {
                var output = cnn.Query<QuizModel>("select * from Quiz", new DynamicParameters());
                return output.ToList();
            }
        }
    }
}
