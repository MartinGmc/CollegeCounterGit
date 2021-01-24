using CollegeCounter.CollegeData.Interface;
using CollegeCounter.DataObjects;
using CollegeCounter.Serializer.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CollegeCounter.CollegeData.Implementation
{
    public class FileCollegeDataLoader : ICollegeDataLoader
    {
        private string _FileToLoad;
        private List<DataReaderError> _Errors;
        private IMySerializer _Serializer;

        public FileCollegeDataLoader(string filename, IMySerializer serializer)
        {
            _FileToLoad = filename;
            _Errors = new List<DataReaderError>();
            this._Serializer = serializer;
        }

        public string GetSerializedErrors()
        {
            return _Serializer.SerializeObject(_Errors);
        }

        public ICollegeData LoadCollegeData(ICollegeData collegeDataToFill)
        {
            var lines = File.ReadAllLines(_FileToLoad);

            bool isInGroup = false; //switch to define if it is in group
            List<string[]> groupContents = new List<string[]>();
            foreach (var line in lines)
            {
                string[] lineSplitted = line.Split(';');
                if (lineSplitted.Length == 1 && lineSplitted[0].Length > 0) // probably name of a group
                {
                    isInGroup = true;
                    groupContents = new List<string[]>();
                    groupContents.Add(lineSplitted);
                }

                else if (lineSplitted.Length == 1 && lineSplitted[0].Length == 0) //probably end of group create objects
                {
                    if (groupContents.Count > 1) // if it is 0 or 1 group has no students, no need to create
                    {
                        Group newGroup = LoadGroupFromLines(groupContents);
                        collegeDataToFill.AddGroup(newGroup);
                    }
                    isInGroup = false;
                }
                else if (lineSplitted.Length > 1) // content of a group
                {
                    if (isInGroup)
                    {
                        groupContents.Add(lineSplitted);
                    }

                }
            }
            if (isInGroup) // probably no blank line on end of file
            {
                if (groupContents.Count > 1) // if it is 0 or 1 group has no students, no need to create
                {
                    Group newGroup = LoadGroupFromLines(groupContents);
                    collegeDataToFill.AddGroup(newGroup);
                }
                isInGroup = false;
            }
            return collegeDataToFill;
        }

        private Group LoadGroupFromLines(List<string[]> lines)
        {
            Group newGroup = new Group();

            foreach (var line in lines)
            {
                if (line.Length == 1) // name of group
                {
                    newGroup.Name = line[0];
                }
                else if (line.Length > 1) // student, probably have some subjects
                {
                    try
                    {
                        Student newStudent = LoadStudentFromLine(line);
                        newGroup.Students.Add(newStudent);
                    }
                    catch (Exception ex)
                    {
                        StringBuilder lineBuilder = new StringBuilder();
                        foreach (var item in line)
                        {
                            lineBuilder.Append(item);
                            lineBuilder.Append(';');
                        }
                        lineBuilder.Remove(lineBuilder.Length - 1, 1);

                        DataReaderError error = new DataReaderError()
                        {
                            ErrorDescription = ex.Message,
                            GroupName = newGroup.Name,
                            LineFaulting = lineBuilder.ToString()
                        };
                        _Errors.Add(error);

                    }
                }
            }
            return newGroup;
        }

        private Student LoadStudentFromLine(string[] input)
        {
            Student newStudent = new Student();
            newStudent.Name = input[0];
            for (int i = 1; i < input.Length; i++)
            {
                Subject newSubject = LoadSubjectFromString(input[i]);
                newStudent.Subjects.Add(newSubject);
            }
            return newStudent;

        }

        private Subject LoadSubjectFromString(string input)
        {
            Subject NewSubject = new Subject();
            string[] splitted = input.Split('=');
            if (splitted.Length == 2) // this is only allowed value
            {
                NewSubject.Name = splitted[0];
                int sucessrate = int.Parse(splitted[1]);
                if (sucessrate < 0 || sucessrate > 100)
                {
                    throw new Exception($"Sucess rate ({sucessrate}) out of range (0 - 100)");
                }
                NewSubject.SucessRate = sucessrate;
            }
            return NewSubject;

        }
    }
}
