using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Page_Navigation_App.Utilities;
using System.Windows.Input;

namespace Page_Navigation_App.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        private string _userName = "Hi You";
        public string UserName { get { return _userName; } set { _userName = value; OnPropertyChanged(); } }
        public ICommand HomeCommand { get; set; }
        public ICommand TranslateCommand { get; set; }
        public ICommand GrammarCommand { get; set; }
        public ICommand QuizzCommand { get; set; }
        public ICommand MyVocabCommand { get; set; }
        public ICommand LearnCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Translate(object obj) => CurrentView = new TranslateVM();
        private void Grammar(object obj) => CurrentView = new GrammarVM();
        private void Quizz(object obj) => CurrentView = new QuizzVM();
        private void MyVocab(object obj) => CurrentView = new MyVocabVM();
        private void Learn(object obj) => CurrentView = new LearnVM();
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            TranslateCommand = new RelayCommand(Translate);
            GrammarCommand = new RelayCommand(Grammar);
            QuizzCommand = new RelayCommand(Quizz);
            MyVocabCommand = new RelayCommand(MyVocab);
            LearnCommand = new RelayCommand(Learn);
            // Startup Page
            CurrentView = new HomeVM();
            if (StaticData.Instance.User != null)
            {
                UserName = $"Hi {StaticData.Instance.User.Name}";
            }
        }
    }
}
