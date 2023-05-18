using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizNoUI.MVVM.Model
{
    class QuizModel
    {
        public string QuizText { get; set; }
        public ObservableCollection<QuestionModel> Questions { get; set; }
        public QuizModel(string quizText, ObservableCollection<QuestionModel> questions)
        {
            QuizText = quizText;
            Questions = questions;
        }
    }
}
