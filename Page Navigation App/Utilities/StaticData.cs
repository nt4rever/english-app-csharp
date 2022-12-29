using Page_Navigation_App.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.Utilities
{
    public class StaticData
    {
        private static StaticData instance = null;
        private static readonly object padlock = new object();
        private User currentUser = null;
        private StaticData() { }
        public static StaticData Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new StaticData();
                    }
                    return instance;
                }
            }
        }

        public User User
        {
            get
            {
                return currentUser;
            }
            set
            {
                currentUser = value;
            }
        }
    }
}
