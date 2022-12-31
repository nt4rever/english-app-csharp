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
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public MyVocabVM()
        {
            dataConnector = new();
            AddCommand = new RelayCommand(AddAction, CanAddAction);
            DeleteCommand = new RelayCommand(DeleteAction, CanDeleteAction);
            UpdateCommand = new RelayCommand(UpdateAction, CanUpdateAction);
            ClearCommand = new RelayCommand(ClearFrom, CanDeleteAction);
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
            else
            {
                SelectedVocab = null;
            }
        }

        private bool CanAddAction(object obj)
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Meaning);
        }

        private bool CanDeleteAction(object obj)
        {
            return SelectedVocab != null;
        }

        private bool CanUpdateAction(object obj)
        {
            return SelectedVocab != null && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Meaning);
        }

        private void ClearFrom(object obj)
        {
            SelectedVocab = null;
            Name = "";
            Meaning = "";
            Note = "";
            Type = "";
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

        private void DeleteAction(object obj)
        {
            if (SelectedVocab == null) return;
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {
                dataConnector.DeleteVocab(SelectedVocab);
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                LoadData();
            };
            b.RunWorkerAsync();
        }

        private void UpdateAction(object obj)
        {
            if (SelectedVocab == null) return;
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {
                dataConnector.UpdateVocab(new Vocab
                {
                    Id = SelectedVocab.Id,
                    Name = Name,
                    Meaning = Meaning,
                    Type = Type,
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
                SelectedVocab = null;
                Name = "";
                Meaning = "";
                Note = "";
                Type = "";
            };
            b.RunWorkerAsync();
        }
    }
}
