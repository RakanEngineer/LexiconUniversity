using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiconUniversity.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public ICollection<Enrollment> Enrollments { get; set; }
        //public int Credits { get; set; }
        // Navigation property
        //public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
