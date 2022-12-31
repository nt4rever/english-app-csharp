using Page_Navigation_App.DataAccess;
using Page_Navigation_App.DataAccess.Model;
using Page_Navigation_App.Utilities;
using Page_Navigation_App.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Page_Navigation_App.ViewModel
{
    class LoginVM : Utilities.ViewModelBase
    {
        private MySqlConnector mySqlConnector;
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
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

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        public LoginVM()
        {
            mySqlConnector = new();
            LoginCommand = new RelayCommand(Login, CanLogin);
            RegisterCommand = new RelayCommand(Register);
            Email = "tai@gmail.com";
        }

        private bool CanLogin(object obj)
        {
            // Add validation logic here
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private void Login(object obj)
        {
            var b = new BackgroundWorker();
            b.DoWork += (o, args) =>
            {
                User user = mySqlConnector.GetUser(Email, Password);
                if (user != null)
                {
                    StaticData.Instance.User = user;
                }
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                if (StaticData.Instance.User != null)
                {
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
                    ErrorMessage = "Email or password incorrect!";
                }
            };
            b.RunWorkerAsync();
        }

        private void Register(object obj)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
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
