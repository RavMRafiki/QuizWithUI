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
using System.Windows.Input;
using System.Windows;

namespace QuizNoUI.MVVM.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Model.AllQuizzesModel allQuizzesModel = new Model.AllQuizzesModel();
        private Model.DatabaseAcces _databaseAcces= new Model.DatabaseAcces();
        public MainViewModel()
        {
            _databaseAcces = new Model.DatabaseAcces();
            setValues();
        }
        private int[] quizids = null;
        private void setValues()
        {
            quizids = _databaseAcces.readDatabase();

            for (int i = 0; i < quizids.Length; i++)
            {
                Quizzes.Add(_databaseAcces.quizName(quizids[i]));
            }
        }


        private int currentQuiz = 0;
        public int CurrentQuiz
        {
            get { return currentQuiz; }
            set
            {
                currentQuiz = value;
                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentQuiz)));
            }
        }
        private string currentQuestionText;
        public string CurrentQuestionText
        {
            get { return currentQuestionText; }
            private set
            {
                currentQuestionText = value;

                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentQuestionText)));
            }
        }
        private int currentQuestion;
        public int CurrentQuestion
        {
            get { return currentQuestion; }
            private set
            {
                currentQuestion = value;

                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentQuestion)));
            }
        }
        private int gameScore;
        public int GameScore
        {
            get { return gameScore; }
            private set
            {
                gameScore = value;

                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameScore)));
            }
        }
        private bool gameStarted;
        public bool GameStarted
        {
            get { return gameStarted; }
            private set
            {
                gameStarted = value;

                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameStarted)));
            }
        }
        private bool gameFinished;
        public bool GameFinished
        {
            get { return gameFinished; }
            private set
            {
                gameFinished = value;

                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GameFinished)));
            }
        }
        private string[] answerstext;
        public string[] AnswersText
        {
            get { return answerstext; }
            private set
            {
                answerstext = value;

                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswersText)));
            }
        }
        private bool[] answers;
        public bool[] Answers
        {
            get { return answers; }
            private set 
            {
                answers = value;

                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answers)));
            }
        }
        private bool[] answersCorrect;
        public bool[] AnswersCorrect
        {
            get { return answersCorrect; }
            private set
            {
                answersCorrect = value;

                //zgłoszenie zmiany wartości tej własności
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswersCorrect)));
            }
        }
        private List<string> quizzes = new List<string>();
        public List<string> Quizzes
        {
            get
            {return quizzes; }
        }
        private List<int> questions;
        private void readQuiz()
        {
            QuizModel idpytania= new QuizModel(quizids[currentQuiz]);
            questions = idpytania.QuestionsIds;
        }

        private void readQuestion()
        {
            QuestionModel questionModel = new QuestionModel(questions.ElementAt(CurrentQuestion));
            CurrentQuestionText= questionModel.QuestionText;

            List<string> _answersText = new List<string>();
            for(int i = 0; i < questionModel.answers.Count; i++)
            {
                _answersText.Add(questionModel.answers.ElementAt(i).AnswerText);
            }
            AnswersText= _answersText.ToArray();

            List<bool> _answers = new List<bool>();
            for (int i = 0; i < questionModel.answers.Count; i++)
            {
                _answers.Add(questionModel.answers.ElementAt(i).IsAnswerTrue);
            }
            AnswersCorrect = _answers.ToArray();
        }
        private ICommand _startGame;

        public ICommand StartGame
        {
            get
            {
                // jesli nie jest określone polecenie to tworzymy je i zwracamy poprozez 
                //pomocniczy typ RelayCommand
                return _startGame ?? (_startGame = new BaseClass.RelayCommand(
                    //co wykonuje polecenie
                    (p) => {
                        readQuiz();
                        GameStarted = true;
                        GameScore =0;
                        CurrentQuestion = 0;
                        GameFinished = false;
                        CurrentQuestionText = string.Empty;
                        Answers = new bool[] { false, false, false, false };
                        AnswersText = new string[] { "1", "2", "3", "4" };
                        readQuestion();
                    }
                    ,
                    //warunek kiedy może je wykonać
                    p => true)
                    );
            }
        }
        private ICommand _stopGame;


        public ICommand StopGame
        {
            get
            {
                // jesli nie jest określone polecenie to tworzymy je i zwracamy poprozez 
                //pomocniczy typ RelayCommand
                return _stopGame ?? (_stopGame = new BaseClass.RelayCommand(
                    //co wykonuje polecenie
                    (p) => {
                        stopthisgame();
                    }
                    ,
                    //warunek kiedy może je wykonać
                    p => true)
                    );
            }
        }
        private void stopthisgame()
        {
            GameStarted = false;
            CurrentQuestion = 0;
            GameFinished = true;
            CurrentQuestionText = "You Finished Quiz";
            Answers = new bool[] { false, false, false, false };
            AnswersText = new string[] { "", "", "", "" };
        }
        private void countthisqustion()
        {
            if (answers.SequenceEqual(answersCorrect))
            {
                GameScore += 1;
            }
        }
        private ICommand _nextQ;


        public ICommand NextQ
        {
            get
            {
                // jesli nie jest określone polecenie to tworzymy je i zwracamy poprozez 
                //pomocniczy typ RelayCommand
                return _nextQ ?? (_nextQ = new BaseClass.RelayCommand(
                    //co wykonuje polecenie
                    (p) => {
                        if (gameFinished) { return; }
                        if (!GameStarted) { return; }
                        countthisqustion();
                        if(CurrentQuestion > questions.Count-2)
                        {
                            stopthisgame();
                            return;
                        }
                        CurrentQuestion+=1;
                        CurrentQuestionText = "Next Question";
                        Answers = new bool[] { false, false, false, false };
                        AnswersText = new string[] { "", "", "", "" };
                        readQuestion();
                    }
                    ,
                    //warunek kiedy może je wykonać
                    p => true)
                    );
            }
        }

    }
}
