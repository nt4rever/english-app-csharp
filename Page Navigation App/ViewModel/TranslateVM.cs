using Page_Navigation_App.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Page_Navigation_App.ViewModel
{
    class TranslateVM : Utilities.ViewModelBase
    {
        private String _originText;
        private String _translateText;
        public String OriginText
        {
            get { return _originText; }
            set
            {
                _originText = value;
                OnPropertyChanged();
            }
        }

        public String TranslateText
        {
            get
            {
                return _translateText;
            }
            set
            {
                _translateText = value;
                OnPropertyChanged();
            }
        }
       
        public TranslateVM()
        {
        }
    }
}
