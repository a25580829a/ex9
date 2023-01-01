using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex9
{
    internal class student
    {
        public string StudentName { get; set; }

        public string StudentID { get; set; }

        public override string ToString()
        {
            return $"{StudentID} {StudentName}";
        }
    }
}
