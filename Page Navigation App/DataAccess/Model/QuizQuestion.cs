using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.DataAccess.Model
{
    public class QuizQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Answers { get; set; }
        public int Correct { get; set; }
        override public string ToString()
        {
            return $"Id: {Id} Text: {Text} Answers: {Answers} CorrectAnswer :{Correct}";
        }
    }
}
