using Page_Navigation_App.Api;
using Page_Navigation_App.DataAccess;
using Page_Navigation_App.DataAccess.Model;
using Page_Navigation_App.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.Design.AxImporter;

namespace Page_Navigation_App.ViewModel
{
    class LearnVM : ViewModelBase
    {
        private MySqlConnector dataConnector;
        private GoogleTranslate googleTranslate;
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
        private string _colorMessage = "#FF0DFF6A";
        public string ColorMessage
        {
            get => _colorMessage;
            set
            {
                _colorMessage = value;
                OnPropertyChanged();
            }
        }
        private string _textBox;
        public string TextBox
        {
            get => _textBox;
            set
            {
                _textBox = value;
                OnPropertyChanged();
            }
        }
        private Vocab _currentVocab = null;
        private bool isRecord = false;
        private int _currentVocabIndex = -1;
        private SpeechRecognitionEngine _recognizer;
        private string _isPronunciationCheck = "Pronunciation Check";
        public string IsPronunciationCheck
        {
            get => _isPronunciationCheck;
            set
            {
                _isPronunciationCheck = value;
                OnPropertyChanged();
            }
        }
        public ICommand StartCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        public ICommand SpellCommand { get; set; }
        public ICommand AnswerCommand { get; set; }
        public ICommand PronunciationCommand { get; set; }
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
            SpellCommand = new RelayCommand(SpellAction);
            AnswerCommand = new RelayCommand(DisplayAnswerAction, CanCheckAction);
            PronunciationCommand = new RelayCommand(StartSpeechRecognition, CanCheckAction);
            googleTranslate = new();
            dataConnector = new();
            InitializeSpeechRecognition();
        }

        private void StartAction(object obj)
        {
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {
                _vocabs = dataConnector.GetVocabsForLearn(SelectedNumberOptionKey, StaticData.Instance.User.Id);
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                LoadGrammar();
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

        private void SpellAction(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(English))
                {
                    googleTranslate.PlayMp3FromUrl("https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&q=" + Uri.EscapeUriString(English) + "&tl=en&total=1&idx=0");
                }
            }
            catch
            {

            }

        }

        private void DisplayAnswerAction(object obj)
        {
            if (_currentVocab != null)
            {
                string note = string.IsNullOrEmpty(_currentVocab.Note) ? "" : $" Note: {_currentVocab.Note}";
                TextBox = $"{_currentVocab.Name} ({_currentVocab.Type}) {_currentVocab.Meaning}{note}";
            }
        }

        private void CheckAnswer(string input, string key)
        {
            string i = TextProcess.RemoveUnicode(input.Trim().ToLower());
            string k = TextProcess.RemoveUnicode(key.Trim().ToLower());
            if (i == k)
            {
                Message = "Correct";
                ColorMessage = "#FF0DFF6A";
            }
            else
            {
                Message = "Incorrect";
                ColorMessage = "red";
            }
        }
        private void SetupVocabTest()
        {
            Message = "";
            TextBox = "";
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

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            TextBox = e.Result.Text;
        }

        private void InitializeSpeechRecognition()
        {
            _recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
            _recognizer.BabbleTimeout = TimeSpan.FromSeconds(1);
            _recognizer.EndSilenceTimeout = TimeSpan.FromSeconds(1);
        }

        private void LoadGrammar()
        {
            Choices words = new Choices();
            _vocabs.ForEach(v =>
            {
                words.Add(v.Name);
            });
            GrammarBuilder builder = new GrammarBuilder();
            builder.Append(words);
            Grammar grammar = new Grammar(builder);
            _recognizer.LoadGrammar(grammar);
        }

        private void StartSpeechRecognition(object obj)
        {
            isRecord = !isRecord;
            if (isRecord)
            {
                TextBox = "...";
                IsPronunciationCheck = "Stop Check";
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }

            else
            {
                IsPronunciationCheck = "Pronunciation Check";
                _recognizer.RecognizeAsyncStop();
            }

        }
    }
}
