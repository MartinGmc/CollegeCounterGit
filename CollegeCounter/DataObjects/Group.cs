using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeCounter.DataObjects
{
    public class Group
    {
        public Group()
        {
            this.Students = new List<Student>();
            this.SubjectResults = new List<SubjectResult>();
        }
        
        public List<Student> Students { get; set; }
        public string  Name { get; set; }
        public List<SubjectResult> SubjectResults { get; set; }
    }
}
