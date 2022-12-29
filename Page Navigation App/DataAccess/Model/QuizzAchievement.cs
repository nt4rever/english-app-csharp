using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.DataAccess.Model
{
    public class QuizzAchievement
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int Question { get; set; }
        public DateTime Time { get; set; }
        public int UserId { get; set; }
    }
}
