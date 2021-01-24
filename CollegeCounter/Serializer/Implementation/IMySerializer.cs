using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeCounter.Serializer.Implementation
{
    public interface IMySerializer
    {
        abstract string SerializeObject(object input);

        abstract string GetFileExtension();
    }
}
