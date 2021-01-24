
using System.Collections.Generic;


namespace CollegeCounter.DataObjects
{
    public class SubjectResult
    {
        public string Name { get; set; }
        public double Average { get; set; }
        public double Median { get; set; }
        public List<int> Modus { get; set; }
    }
}
