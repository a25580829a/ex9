using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex9
{
    internal class Course
    {
        public string CourseName { get; set; }

        public string Type { get; set; }

        public int Point { get; set; }

        public string OpeningClass { get; set; }

        public Teacher Tutor { get; set; }

        public Course(Teacher tutor)
        {
            Tutor = tutor;
        }
    }
}
