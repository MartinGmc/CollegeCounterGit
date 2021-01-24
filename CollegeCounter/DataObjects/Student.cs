using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeCounter.DataObjects
{
    public class Student
    {
        public Student()
        {
            this.Subjects = new List<Subject>();
        }
        
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }
        public double WeightedAverage { get; set; }
        
    }
}
