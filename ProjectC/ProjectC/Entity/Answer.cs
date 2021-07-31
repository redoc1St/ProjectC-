using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectC.Entity
{
    class Answer
    {
        private float mark;
        private bool isPassed;
        public float Mark { get => mark; set => mark = value; }
        public bool IsPassed { get => isPassed; set => isPassed = value; }
    }
}
