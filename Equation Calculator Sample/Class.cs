using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equation_Calculator_Sample
{
    internal class Equation
    {
        public string equationID { get; set; } = "";
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public decimal finalResult { get; set; }
        public Equation(string strID, int _startIndex, int _endIndex) {
            equationID = strID;
            startIndex = _startIndex;
            endIndex = _endIndex;
        }

    }

    internal class ArrayItem { 
        public int index { get; set; }
        public string content { get; set; } = "";
        public ArrayItem(int _index, string _content) 
        {
            index = _index;
            content = _content;
        }
    }
}
