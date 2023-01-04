using Page_Navigation_App.DataAccess;
using Page_Navigation_App.DataAccess.Model;
using Page_Navigation_App.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace Page_Navigation_App.ViewModel
{
    class HomeVM : Utilities.ViewModelBase
    {
        private MySqlConnector dataConnector;

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

        private string _textVocab;
        public string TextVocab
        {
            get => _textVocab;
            set
            {
                _textVocab = value;
                OnPropertyChanged();
            }
        }
        private string _textQuizz;
        public string TextQuizz
        {
            get => _textQuizz;
            set
            {
                _textQuizz = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startDate = DateTime.Now.AddDays(-30);
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }
        private List<ISeries> series;
        public List<ISeries> Series
        {
            get => series;
            set
            {
                series = value;
                OnPropertyChanged();
            }
        }

        private List<Axis> _xaxes;
        public List<Axis> XAxes
        {
            get => _xaxes; set
            {
                _xaxes = value;
                OnPropertyChanged();
            }
        }
        private List<Axis> _yaxes;
        public List<Axis> YAxes
        {
            get => _yaxes;
            set
            {
                _yaxes = value;
                OnPropertyChanged();
            }
        }

        public SolidColorPaint LegendTextPaint { get; set; } =
        new SolidColorPaint
        {
            Color = new SKColor(222, 222, 222),
            //SKTypeface = SKTypeface.FromFamilyName("Courier New")
        };

        public SolidColorPaint LedgendBackgroundPaint { get; set; } =
            new SolidColorPaint(new SKColor(240, 240, 240));

        public ICommand EditCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public HomeVM()
        {
            Name = StaticData.Instance.User.Name;
            Email = StaticData.Instance.User.Email;
            EditCommand = new RelayCommand(EditAction, CanEdit);
            SearchCommand = new RelayCommand(SearchAction);
            dataConnector = new();
            LoadData();
        }

        private bool CanEdit(object obj)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private void EditAction(object obj)
        {
            var b = new BackgroundWorker();
            bool res = true;
            b.DoWork += (o, args) =>
            {
                StaticData.Instance.User.Name = Name;
                res = dataConnector.UpdateUser(new User
                {
                    Id = StaticData.Instance.User.Id,
                    Name = Name,
                    Password = Password
                });
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                if (!res)
                {
                    MessageBox.Show("Error!");
                }
            };
            b.RunWorkerAsync();
        }

        private void SearchAction(object obj)
        {
            Console.WriteLine($"{StartDate} {EndDate}");
            LoadData();
        }

        private void LoadData()
        {
            var b = new BackgroundWorker();
            Statistic statistic = null;
            b.DoWork += (o, args) =>
            {
                statistic = dataConnector.GetInfoDashboard(StaticData.Instance.User.Id, StartDate, EndDate);
            };
            b.RunWorkerCompleted += (o, args) =>
            {
                if (statistic != null)
                {
                    TextVocab = $"{statistic.VocabCount} vocab";
                    TextQuizz = $"{statistic.ScoreCount}/{statistic.QuestionCount} quizz";
                    List<double> scoreArr = new();
                    List<double> questionArr = new();
                    List<string> labelArr = new();
                    if (statistic.ChartElements != null)
                        statistic.ChartElements.ForEach(ele =>
                            {
                                scoreArr.Add(ele.Score);
                                questionArr.Add(ele.Question);
                                labelArr.Add(ele.Date.ToString("dd/MM"));
                            });
                    Series = new List<ISeries>
                    {
                        new ColumnSeries<double>
                        {
                            Name = "Score",
                            Values = scoreArr
                        },
                        new ColumnSeries<double>
                        {
                            Name = "Question",
                            Values = questionArr
                        }
                    };

                    XAxes = new List<Axis>
                    {
                        new Axis
                        {
                            Labels = labelArr,
                            LabelsRotation = 0,
                            SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                            SeparatorsAtCenter = false,
                            TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                            TicksAtCenter = true,
                            LabelsPaint = new SolidColorPaint(SKColors.White),
                        }
                    };
                    YAxes = new List<Axis>
                    {
                        new Axis
                        {
                            LabelsPaint = new SolidColorPaint(SKColors.White),
                        }
                    };
                }
            };
            b.RunWorkerAsync();
        }
    }
}
