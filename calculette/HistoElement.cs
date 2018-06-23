using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculette
{
    public class HistoElement
    {
        private String operation;
        private String result;

        public string Operation { get => operation; set => operation = value; }
        public string Result { get => result; set => result = value; }

        public HistoElement(String op, String re)
        {
            operation = op;
            result = re;
        }
    }
}
