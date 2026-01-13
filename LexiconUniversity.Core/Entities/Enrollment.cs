using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconUniversity.Core.Entities
{
    public class Enrollment
    {
        public int Grade { get; set; }

        //Foreign keys
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        //Navigational properties
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
