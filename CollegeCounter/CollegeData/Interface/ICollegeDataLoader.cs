using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeCounter.CollegeData.Interface
{
    public interface ICollegeDataLoader
    {
        /// <summary>
        /// load college data and fill it into provided object
        /// </summary>
        /// <param name="collegeDataToFill">object which will be filled with collegeData</param>
        /// <returns></returns>
        abstract ICollegeData LoadCollegeData(ICollegeData collegeDataToFill);

        /// <summary>
        /// gets serialized errors from class
        /// </summary>
        /// <returns></returns>
        abstract string GetSerializedErrors();

    }
}
