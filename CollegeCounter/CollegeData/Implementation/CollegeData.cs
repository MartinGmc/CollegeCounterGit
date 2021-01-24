using CollegeCounter.CollegeData.Interface;
using CollegeCounter.DataObjects;
using CollegeCounter.MathHelper;
using CollegeCounter.Serializer.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace CollegeCounter.CollegeData.Implementation
{
    public class CollegeData : ICollegeData
    {
        private List<SubjectWeight> SubjectWeights;
        private List<Group> Groups;
        private List<SubjectResult> totalResults;
        private IMySerializer serializer;

        public CollegeData (CollegeDataSettings settings, IMySerializer serializer)
        {
            Groups = new List<Group>();
            totalResults = new List<SubjectResult>();
            SubjectWeights = settings.Subjectweights;
            this.serializer = serializer;
        }
        
        public void AddGroup(Group group)
        {
            Groups.Add(group);
        }

        public string GetSerializedGroups()
        {
            return serializer.SerializeObject(Groups);
        }

        public string GetSerializedOverall()
        {
            return serializer.SerializeObject(totalResults);
        }

        public bool Process(string outputFolder = null, string outputType = null)
        {
            foreach (var group in Groups)
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
                        if (!subjects.Contains(subject.Name)) subjects.Add(subject.Name);
                    }
                }
                //now when I know all the subjects relevant for group, can start calculation
                foreach (var subj in subjects)
                {
                    //get all values for this subject in group
                    var values = group.Students.Select(p => p.Subjects.Where(x => x.Name == subj).DefaultIfEmpty(new Subject() { Name= subj,SucessRate = 0 }).First().SucessRate).ToArray();
                    
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
            foreach (var grp in Groups)
            {
                foreach (var item in grp.SubjectResults)
                {
                    if (!subjectsOverall.Contains(item.Name)) subjectsOverall.Add(item.Name);
                }
            }

            //now start calculation
            foreach (var subj in subjectsOverall)
            {
                List<int> totalvalues = new List<int>();
                var valuesInGroups = Groups.Select(u=> u.Students.Select(p => p.Subjects.Where(x => x.Name == subj).DefaultIfEmpty(new Subject() { Name = subj, SucessRate = 0 }).First().SucessRate).ToList());
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
                totalResults.Add(newResult);
                
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
            int[] weights = new int[SubjectWeights.Count];
            int[] values = new int[SubjectWeights.Count];
            for (int i = 0; i < SubjectWeights.Count; i++)
            {
                weights[i] = SubjectWeights[i].WeightPercentage;
                var subject = student.Subjects.FirstOrDefault(p => p.Name == SubjectWeights[i].Name);
                int SubjectPercentage = 0;
                if (subject != null) SubjectPercentage = subject.SucessRate;
                values[i] = SubjectPercentage;
            }
            return StatFunctions.CalculateWeightedAverage(values, weights);
        }
    }
}
