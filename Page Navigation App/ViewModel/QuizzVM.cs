using Page_Navigation_App.DataAccess;
using Page_Navigation_App.DataAccess.Model;
using Page_Navigation_App.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Page_Navigation_App.ViewModel
{
    class QuizzVM : ViewModelBase
    {
        readonly MySqlConnector dataConnector;
        private QuizQuestion[] _questions = {
            new QuizQuestion
            {
                Text = "The company's ___ policy requires all employees to wear a uniform.",
                Answers = "A. dress, B. clothing, C. attire, D. garb",
                CorrectAnswer = 0
            }};
        private int _currentQuestionIndex = 0;
        public QuizQuestion CurrentQuestion => _questions[_currentQuestionIndex];
        public string[] Answers => CurrentQuestion.Answers.Split(",").Select(x => x.Trim()).ToArray();
        private string _selectedAnswer;
        public string SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                _selectedAnswer = value;
                OnPropertyChanged();
            }
        }

        private bool _openPopup = false;
        public bool OpenPopup
        {
            get { return _openPopup; }
            set
            {
                _openPopup = value;
                OnPropertyChanged();
            }
        }

        private string _textPopup;
        public string TextPopup
        {
            get { return _textPopup; }
            set
            {
                _textPopup = value;
                OnPropertyChanged();
            }
        }

        private string _colorPopup = "red";
        public string ColorPopup
        {
            get { return _colorPopup; }
            set
            {
                _colorPopup = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private string _isQuizz = "Hidden";
        public string IsQuizz
        {
            get => _isQuizz;
            set
            {
                _isQuizz = value;
                OnPropertyChanged();
            }
        }
        private string _isButtonLoad = "Visible";
        public string IsButtonLoad
        {
            get => _isButtonLoad;
            set
            {
                _isButtonLoad = value;
                OnPropertyChanged();
            }
        }
        private int _score = 0;
        public ObservableCollection<KeyValuePair<int, string>> Options { get; set; }
        public int SelectedOptionKey { get; set; }
        public ICommand NextQuestionCommand { get; set; }
        public ICommand SubmitAnswerCommand { get; set; }
        public ICommand ClosePopupCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public QuizzVM()
        {
            dataConnector = new MySqlConnector();
            NextQuestionCommand = new RelayCommand(NextQuestion);
            SubmitAnswerCommand = new RelayCommand(SubmitAnswer);
            ClosePopupCommand = new RelayCommand(ClosePopUp);
            RefreshCommand = new RelayCommand(Refresh);
            Options = new ObservableCollection<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(5, "5 question"),
                new KeyValuePair<int, string>(10, "10 question"),
                new KeyValuePair<int, string>(20, "20 question")
            };
            SelectedOptionKey = 5;
        }

        private void Refresh(object obj)
        {
            IsLoading = true;
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {

                var a = dataConnector.GetQuizQuestionsAsync(SelectedOptionKey);
                _questions = a.Result.ToArray();

            };
            b.RunWorkerCompleted += (o, args) =>
            {
                IsQuizz = "Visible";
                IsButtonLoad = "Hidden";
                IsLoading = false;
            };
            b.RunWorkerAsync();
        }

        private void NextQuestion(object obj)
        {
            OpenPopup = false;
            _currentQuestionIndex++;
            if (_currentQuestionIndex == _questions.Length)
            {
                _currentQuestionIndex = 0;
                TextPopup = $"Your has completed this quizz! {_score}/{_questions.Length}";
                InsertAchievement(new QuizzAchievement
                {
                    Score = _score,
                    Question = _questions.Length,
                    UserId = StaticData.Instance.User.Id,
                    Time = DateTime.Now
                });
                ColorPopup = "green";
                OpenPopup = true;
                _score = 0;
            }

            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(Answers));
            SelectedAnswer = null;
        }

        private void SubmitAnswer(object obj)
        {
            int key = CurrentQuestion.CorrectAnswer;
            string value = Answers[key];
            if (SelectedAnswer == value)
            {
                _score++;
                TextPopup = "Your answer correct";
                ColorPopup = "green";
                OpenPopup = true;
            }
            else
            {
                TextPopup = "Your answer incorrect";
                ColorPopup = "red";
                OpenPopup = true;
            }
        }

        private void ClosePopUp(object obj)
        {
            OpenPopup = false;
        }

        private void InsertAchievement(QuizzAchievement achievement)
        {
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {
                dataConnector.InsertAchievement(achievement);
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                //on completed
            };
            b.RunWorkerAsync();
        }
    }
}
