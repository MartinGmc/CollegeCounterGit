using CollegeCounter.DataObjects;

namespace CollegeCounter.CollegeData.Interface
{
    public interface ICollegeData
    {
        /// <summary>
        /// add new group into collegeData
        /// </summary>
        /// <param name="group"></param>
        abstract void AddGroup(Group group);

        /// <summary>
        /// process and calculate all statistics for all groups and overall
        /// </summary>
        /// <param name="outputFolder"></param>
        /// <param name="outputType"></param>
        /// <returns></returns>
        abstract bool Process(string outputFolder = null, string outputType = null);

        /// <summary>
        /// return serailize output for every group
        /// </summary>
        /// <returns></returns>
        abstract string GetSerializedGroups();

        /// <summary>
        /// return serialized overall results
        /// </summary>
        /// <returns></returns>
        abstract string GetSerializedOverall();
    }
}
