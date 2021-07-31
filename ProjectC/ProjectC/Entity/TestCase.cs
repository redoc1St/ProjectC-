using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectC.Entity
{
    class TestCase
    {
        private int numOfQuestion;
        private List<string> input;
        private string output;
        private float mark;

        public TestCase()
        {
            this.input = new List<string>();
            this.Output = "";
            this.numOfQuestion = 0;
        }

        public List<string> Input { get => input; set => input = value; }
        public string Output { get => output; set => output = value; }
        public float Mark { get => mark; set => mark = value; }
        public int NumOfQuestion { get => numOfQuestion; set => numOfQuestion = value; }
    }
}
