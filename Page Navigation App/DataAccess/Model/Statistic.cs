using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.DataAccess.Model
{
    public class Statistic
    {
        public int VocabCount { get; set; }
        public int ScoreCount { get; set; }
        public int QuestionCount { get; set; }
        public List<ChartElement> ChartElements { get; set; }
    }

    public class ChartElement
    {
        public int Score { get; set; }
        public int Question { get; set; }
        public DateTime Date { get; set; }
    }
}
