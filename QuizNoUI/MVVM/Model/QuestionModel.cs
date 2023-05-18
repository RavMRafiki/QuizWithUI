using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizNoUI.MVVM.Model
{
    public class QuestionModel
    {
        public string QuestionText { get; set; }
        public ObservableCollection<AnswerModel> Answers { get; set; }
        public QuestionModel(string questionText, ObservableCollection<AnswerModel> answers)
        {
            QuestionText = questionText;
            Answers = answers;
        }   
    }
}
