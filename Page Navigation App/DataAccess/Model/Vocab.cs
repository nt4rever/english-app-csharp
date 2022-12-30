using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Page_Navigation_App.DataAccess.Model
{
    public class Vocab
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
    }
}
