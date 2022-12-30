using Page_Navigation_App.DataAccess;
using Page_Navigation_App.DataAccess.Model;
using Page_Navigation_App.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using System.Windows.Controls;
using System.Windows;

namespace Page_Navigation_App.ViewModel
{
    class MyVocabVM : ViewModelBase
    {
        private MySqlConnector dataConnector;
        private List<Vocab> _vocab;
        public List<Vocab> Vocabs
        {
            get => _vocab;
            set
            {
                _vocab = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _type;
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        private string _meaning;
        public string Meaning
        {
            get => _meaning;
            set
            {
                _meaning = value;
                OnPropertyChanged("Meaning");
            }
        }
        private string _note;
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged("Note");
            }
        }
        public Vocab SelectedVocab { get; set; }
        public ICommand AddCommand { get; set; }
        public MyVocabVM()
        {
            dataConnector = new();
            AddCommand = new RelayCommand(AddAction, CanAddAction);
            LoadData();
        }

        public void OnSelectionChange()
        {
            if (SelectedVocab != null)
            {
                Name = SelectedVocab.Name;
                Type = SelectedVocab.Type;
                Meaning = SelectedVocab.Meaning;
                Note = SelectedVocab.Note;
            }
        }

        private bool CanAddAction(object obj)
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Meaning);
        }

        private void AddAction(object obj)
        {
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {
                dataConnector.InsertVocab(new Vocab
                {
                    UserId = StaticData.Instance.User.Id,
                    Name = Name,
                    Type = Type,
                    Meaning = Meaning,
                    Note = Note
                });
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                LoadData();
            };
            b.RunWorkerAsync();
        }

        private void LoadData()
        {
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {
                Vocabs = dataConnector.GetVocabs();
            };
            b.RunWorkerCompleted += (o, args) =>
            {

            };
            b.RunWorkerAsync();
        }
    }
}
