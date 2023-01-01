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
        private List<QuizQuestion> _quizzes;
        private int _currentQuizzIndex = -1;
        private QuizQuestion _currentQuizz = null;
        public QuizQuestion CurrentQuizz
        {
            get => _currentQuizz;
            set
            {
                _currentQuizz = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _answer;
        public ObservableCollection<string> Answers
        {
            get => _answer;
            set
            {
                _answer = value;
                OnPropertyChanged();
            }
        }

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
        public ICommand RefreshCommand { get; set; }

        public QuizzVM()
        {
            dataConnector = new MySqlConnector();
            NextQuestionCommand = new RelayCommand(NextQuestion);
            SubmitAnswerCommand = new RelayCommand(SubmitAnswer);
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
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {

                var a = dataConnector.GetQuizQuestionsAsync(SelectedOptionKey);
                _quizzes = a.Result.ToList();
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                IsQuizz = "Visible";
                IsButtonLoad = "Hidden";
                _currentQuizzIndex = 0;
                SetupQuizz();
            };
            b.RunWorkerAsync();
        }

        private void NextQuestion(object obj)
        {
            OpenPopup = false;
            _currentQuizzIndex++;
            if (_currentQuizzIndex != -1 && _currentQuizzIndex < _quizzes.Count)
            {
                SetupQuizz();
            }
            else if (_currentQuizzIndex == _quizzes.Count)
            {
                TextPopup = $"Your has completed this quizz! {_score}/{_quizzes.Count}";
                InsertAchievement(new QuizzAchievement
                {
                    Score = _score,
                    Question = _quizzes.Count,
                    UserId = StaticData.Instance.User.Id,
                    Time = DateTime.Now
                });
                ColorPopup = "green";
                OpenPopup = true;
                _score = 0;
                IsQuizz = "Hidden";
                IsButtonLoad = "Visible";
            }

            OnPropertyChanged(nameof(CurrentQuizz));
            OnPropertyChanged(nameof(Answers));
            SelectedAnswer = null;
        }

        private void SubmitAnswer(object obj)
        {
            int key = CurrentQuizz.Correct;
            string value = Answers.ElementAt(key);
            if (SelectedAnswer == value)
            {
                _score++;
                TextPopup = "Your answer correct 👏";
                ColorPopup = "green";
                OpenPopup = true;
            }
            else
            {
                TextPopup = $"Oops, {value} is the correct answer";
                ColorPopup = "red";
                OpenPopup = true;
            }
        }

        private void SetupQuizz()
        {
            OpenPopup = false;
            TextPopup = "";
            CurrentQuizz = _quizzes.ElementAt(_currentQuizzIndex);
            Answers = new ObservableCollection<string>(CurrentQuizz.Answers.Split(",").Select(x => x.Trim()));
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
