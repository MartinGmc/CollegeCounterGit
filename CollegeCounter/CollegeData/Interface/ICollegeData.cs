using CollegeCounter.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeCounter.CollegeData.Interface
{
    public interface ICollegeData
    {
        abstract void AddGroup(Group group);
        
        abstract bool Process(string outputFolder = null, string outputType = null);

        abstract string GetSerializedGroups();

        abstract string GetSerializedOverall();
    }
}
