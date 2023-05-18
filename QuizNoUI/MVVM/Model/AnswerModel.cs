using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizNoUI.MVVM.Model
{
    public class AnswerModel
    {
        public string AnswerText { get; set; }
        public bool IsAnswerTrue { get; set; }
        public AnswerModel(string answerText, bool isAnswerTrue)
        {
            AnswerText = answerText;
            IsAnswerTrue = isAnswerTrue;
        }   
    }
}
