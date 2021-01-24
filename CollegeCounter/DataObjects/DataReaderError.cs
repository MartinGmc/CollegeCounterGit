using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeCounter.DataObjects
{
    public class DataReaderError
    {
        public string ErrorDescription { get; set; }
        public string GroupName { get; set; }
        public string LineFaulting { get; set; }
    }
}
