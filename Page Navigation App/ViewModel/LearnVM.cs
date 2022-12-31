using Page_Navigation_App.DataAccess;
using Page_Navigation_App.DataAccess.Model;
using Page_Navigation_App.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.Design.AxImporter;

namespace Page_Navigation_App.ViewModel
{
    class LearnVM : ViewModelBase
    {
        private MySqlConnector dataConnector;
        private List<Vocab> _vocabs;
        public ObservableCollection<KeyValuePair<int, string>> NumberOptions { get; set; }
        public int SelectedNumberOptionKey { get; set; }
        public ObservableCollection<KeyValuePair<int, string>> LangOptions { get; set; }
        public int SelectedLangOptionKey { get; set; }
        private string _english;
        public string English
        {
            get => _english;
            set
            {
                _english = value;
                OnPropertyChanged();
            }
        }
        private string _vietnamese;
        public string Vietnamese
        {
            get => _vietnamese;
            set
            {
                _vietnamese = value;
                OnPropertyChanged();
            }
        }
        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }
        private Vocab _currentVocab = null;
        private int _currentVocabIndex = -1;
        public ICommand StartCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        public LearnVM()
        {
            NumberOptions = new ObservableCollection<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(5, "5 vocab"),
                new KeyValuePair<int, string>(10, "10 vocab"),
                new KeyValuePair<int, string>(20, "20 vocab")
            };
            SelectedNumberOptionKey = 5;
            LangOptions = new ObservableCollection<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(0, "Vietnamese"),
                new KeyValuePair<int, string>(10, "English")
            };
            SelectedLangOptionKey = 0;
            StartCommand = new RelayCommand(StartAction);
            NextCommand = new RelayCommand(NextAction, CanNextAction);
            CheckCommand = new RelayCommand(CheckAction, CanCheckAction);
            dataConnector = new();
        }

        private void StartAction(object obj)
        {
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {
                _vocabs = dataConnector.GetVocabsForLearn(SelectedNumberOptionKey);
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                if (_vocabs.Count > 0)
                {
                    _currentVocabIndex = 0;
                    SetupVocabTest();
                }
            };
            b.RunWorkerAsync();
        }

        private bool CanNextAction(object obj)
        {
            return (_vocabs != null) && (_vocabs.Count > 0);
        }

        private void NextAction(object obj)
        {
            _currentVocabIndex++;
            if (_currentVocabIndex != -1 && _currentVocabIndex < _vocabs.Count)
            {
                SetupVocabTest();
            }
            else if (_currentVocabIndex != -1)
            {
                _currentVocabIndex = 0;
                SetupVocabTest();
            }
        }

        private void CheckAction(object obj)
        {
            if (SelectedLangOptionKey == 0)
                CheckAnswer(Vietnamese, _currentVocab.Meaning);
            else
                CheckAnswer(English, _currentVocab.Name);
        }

        private bool CanCheckAction(object obj)
        {
            return _currentVocab != null;
        }

        private void CheckAnswer(string input, string key)
        {
            string i = input.Trim().ToLower();
            string k = key.Trim().ToLower();
            if (i == k)
            {
                Message = "Correct";
            }
            else
            {
                Message = "Incorrect";
            }
        }
        private void SetupVocabTest()
        {
            Message = "";
            _currentVocab = _vocabs.ElementAt(_currentVocabIndex);
            if (SelectedLangOptionKey == 0)
            {
                English = _currentVocab.Name;
                Vietnamese = "";
            }
            else
            {
                English = "";
                Vietnamese = _currentVocab.Meaning;
            }
        }
    }
}
