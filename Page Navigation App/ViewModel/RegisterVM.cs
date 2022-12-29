using Page_Navigation_App.DataAccess;
using Page_Navigation_App.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Page_Navigation_App.ViewModel
{
    class RegisterVM : Utilities.ViewModelBase
    {
        private readonly MySqlConnector mySqlConnector;
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; set; }
        public ICommand LoginCommand { get; set; }

        public RegisterVM()
        {
            mySqlConnector = new();
            RegisterCommand = new RelayCommand(Register, CanRegister);
            LoginCommand = new RelayCommand(Login);
        }
        private bool CanRegister(object obj)
        {
            // Add validation logic here
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Name);
        }

        private void Register(object obj)
        {
            var b = new BackgroundWorker();
            bool res = false;
            b.DoWork += (o, args) =>
            {
                res = mySqlConnector.Register(new DataAccess.Model.User() { Name = Name, Email = Email, Password = Password });
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                if (res)
                {
                    StaticData.Instance.User = new DataAccess.Model.User() { Name = Name, Email = Email, Password = Password };
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    var window = Application.Current.Windows
                        .OfType<Window>()
                        .SingleOrDefault(x => x.DataContext == this);

                    if (window != null)
                    {
                        window.Close();
                    }
                }
                else
                {
                    ErrorMessage = "Register Failed!";
                }
            };
            b.RunWorkerAsync();
        }

        private void Login(object obj)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            var window = Application.Current.Windows
            .OfType<Window>()
            .SingleOrDefault(x => x.DataContext == this);
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
