using CollegeCounter.CollegeData.Implementation;
using CollegeCounter.DataObjects;
using CollegeCounter.Serializer.Implementation;
using CollegeCounter.Serializer.Interface;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CollegeCounterTests
{
    class CollegeDataTests
    {
        CollegeDataSettings settings;

        [SetUp]
        public void Setup()
        {
            List<SubjectWeight> subjectWeights = new List<SubjectWeight>();

            SubjectWeight newWeight = new SubjectWeight()
            {
                Name = "AAA",
                WeightPercentage = 10
            };
            subjectWeights.Add(newWeight);
            
            settings = new CollegeDataSettings() { Subjectweights = subjectWeights};
        }

        /// <summary>
        /// Test if add new group will be sucessful
        /// fails only if throws an exception
        /// </summary>
        [Test]
        public void CollegeDataAddGroupTest()
        {
            CollegeData colData = new CollegeData(settings, null);

            Group g1 = new Group() { Name = "grp",
                Students = new List<Student>()
            {
                new Student()
                {
                    Name= "std1",
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name="AAA",
                            SucessRate = 6
                        }
                    }
                }
            }
            };

            colData.AddGroup(g1);

            Assert.Pass();
        }

        /// <summary>
        /// Test if process will be sucessful
        /// fails only if throws an exception
        /// </summary>
        [Test]
        public void CollegeDataProcessTest()
        {
            CollegeData colData = new CollegeData(settings, null);

            Group g1 = new Group()
            {
                Name = "grp",
                Students = new List<Student>()
            {
                new Student()
                {
                    Name= "std1",
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name="AAA",
                            SucessRate = 6
                        }
                    }
                }
            }
            };

            colData.AddGroup(g1);
            colData.Process();

            Assert.Pass();
        }

        /// <summary>
        /// Test if serialization will be sucessful
        /// fails only if throws an exception
        /// </summary>
        [Test]
        public void CollegeDataOverallSerializationTest()
        {
            IMySerializer ser = new MyJsonSerializer();
            CollegeData colData = new CollegeData(settings, ser);

            Group g1 = new Group()
            {
                Name = "grp",
                Students = new List<Student>()
            {
                new Student()
                {
                    Name= "std1",
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name="AAA",
                            SucessRate = 6
                        }
                    }
                }
            }
            };

            colData.AddGroup(g1);
            colData.Process();

            var str = colData.GetSerializedOverall();

            Assert.IsTrue(str.Length>0);
        }

        /// <summary>
        /// Test if serialization will be sucessful
        /// fails only if throws an exception
        /// </summary>
        [Test]
        public void CollegeDataResultlSerializationTest()
        {
            IMySerializer ser = new MyJsonSerializer();
            CollegeData colData = new CollegeData(settings, ser);

            Group g1 = new Group()
            {
                Name = "grp",
                Students = new List<Student>()
            {
                new Student()
                {
                    Name= "std1",
                    Subjects = new List<Subject>()
                    {
                        new Subject()
                        {
                            Name="AAA",
                            SucessRate = 6
                        }
                    }
                }
            }
            };

            colData.AddGroup(g1);
            colData.Process();

            var str = colData.GetSerializedGroups();

            Assert.IsTrue(str.Length > 0);
        }
    }
}
