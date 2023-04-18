using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class DOC
    {
        public DOC()
        {

        }

        public DOC(string name, string status)
        {
            NAME = name;
            STATUS = status;

        }



        public int ID { get; set; }
        public string NAME { get; set; }
        public string STATUS { get; set; }

        public override string ToString()
        {
            return $"{ID} NAME {NAME} STATUS {STATUS}";
        }
    }
}
