using CollegeCounter.CollegeData.Interface;
using CollegeCounter.DataObjects;
using CollegeCounter.MathHelper;
using CollegeCounter.Serializer.Implementation;
using System.Collections.Generic;
using System.Linq;

namespace CollegeCounter.CollegeData.Implementation
{
    public class CollegeData : ICollegeData
    {
        private List<SubjectWeight> _SubjectWeights;
        private List<Group> _Groups;
        private List<SubjectResult> _TotalResults;
        private IMySerializer _Serializer;

        public CollegeData(CollegeDataSettings settings, IMySerializer serializer)
        {
            _Groups = new List<Group>();
            _TotalResults = new List<SubjectResult>();
            _SubjectWeights = settings.Subjectweights;
            this._Serializer = serializer;
        }

        public void AddGroup(Group group)
        {
            _Groups.Add(group);
        }

        public string GetSerializedGroups()
        {
            return _Serializer.SerializeObject(_Groups);
        }

        public string GetSerializedOverall()
        {
            return _Serializer.SerializeObject(_TotalResults);
        }

        public bool Process(string outputFolder = null, string outputType = null)
        {
            foreach (var group in _Groups)
            {
                List<string> subjects = new List<string>();
                foreach (var std in group.Students)
                {
                    //calculate weighted average for each student in each group
                    std.WeightedAverage = CalculateStudentWeightedAverage(std);
                    //need to fill in list of subjects I assume that each group can have different subjects, 
                    //if at least one person have subject in group then subject is relevant for group 
                    foreach (var subject in std.Subjects)
                    {
                        if (!subjects.Contains(subject.Name))
                        {
                            subjects.Add(subject.Name);
                        }
                    }
                }
                //now when I know all the subjects relevant for group, can start calculation
                foreach (var subj in subjects)
                {
                    //get all values for this subject in group
                    var values = group.Students.Select(p => p.Subjects.Where(x => x.Name == subj).DefaultIfEmpty(new Subject() { Name = subj, SucessRate = 0 }).First().SucessRate).ToArray();

                    SubjectResult result = new SubjectResult();
                    result.Name = subj;
                    result.Average = StatFunctions.CalculateAverage(values);
                    result.Median = StatFunctions.CalculateMedian(values);
                    result.Modus = StatFunctions.CalculateModeValue(values);
                    group.SubjectResults.Add(result);
                }

            }
            //now calculate overall results

            //first build of list of relevant subjects
            List<string> subjectsOverall = new List<string>();
            foreach (var grp in _Groups)
            {
                foreach (var item in grp.SubjectResults)
                {
                    if (!subjectsOverall.Contains(item.Name))
                    {
                        subjectsOverall.Add(item.Name);
                    }
                }
            }

            //now start calculation
            foreach (var subj in subjectsOverall)
            {
                List<int> totalvalues = new List<int>();
                var valuesInGroups = _Groups.Select(u => u.Students.Select(p => p.Subjects.Where(x => x.Name == subj).DefaultIfEmpty(new Subject() { Name = subj, SucessRate = 0 }).First().SucessRate).ToList());
                foreach (var item in valuesInGroups)
                {
                    totalvalues.AddRange(item);
                }
                int[] totalValuesArray = totalvalues.ToArray();
                SubjectResult newResult = new SubjectResult();
                newResult.Name = subj;
                newResult.Average = StatFunctions.CalculateAverage(totalValuesArray);
                newResult.Median = StatFunctions.CalculateMedian(totalValuesArray);
                newResult.Modus = StatFunctions.CalculateModeValue(totalValuesArray);
                _TotalResults.Add(newResult);

            }
            //everything calculated at this point

            return true;
        }

        /// <summary>
        /// return calculated weighted average for student 
        /// </summary>
        /// <param name="student"></param>
        private double CalculateStudentWeightedAverage(Student student)
        {
            int[] weights = new int[_SubjectWeights.Count];
            int[] values = new int[_SubjectWeights.Count];
            for (int i = 0; i < _SubjectWeights.Count; i++)
            {
                weights[i] = _SubjectWeights[i].WeightPercentage;
                var subject = student.Subjects.FirstOrDefault(p => p.Name == _SubjectWeights[i].Name);
                int SubjectPercentage = 0;
                if (subject != null)
                {
                    SubjectPercentage = subject.SucessRate;
                }
                values[i] = SubjectPercentage;
            }
            return StatFunctions.CalculateWeightedAverage(values, weights);
        }
    }
}
