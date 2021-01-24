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
    class FileCollegeDataLoaderTests
    {
        private CollegeDataSettings settings;

        [SetUp]
        public void Setup()
        {
            settings = new CollegeDataSettings();
        }

        /// <summary>
        /// Test of initialization Loader
        /// fails only if init throws an exception
        /// </summary>
        [Test]
        public void FileCollegeDataLoaderInitTest()
        {
            FileCollegeDataLoader cat = new FileCollegeDataLoader("SampleData.txt",null);
            Assert.Pass();
        }

        [Test]
        public void FileCollegeDataLoaderLoadTest()
        {
            CollegeData testCollegeData = new CollegeData(settings,null);

            FileCollegeDataLoader loader = new FileCollegeDataLoader("SampleData.txt",null);
            var outputCollegeData = loader.LoadCollegeData(testCollegeData);

            Assert.Pass();
        }

        [Test]
        public void FileCollegeDataLoaderSerializerJsonTest()
        {
            IMySerializer ser = new MyJsonSerializer();
            CollegeData testCollegeData = new CollegeData(settings, ser);

            FileCollegeDataLoader loader = new FileCollegeDataLoader("SampleData.txt", ser);
            var outputCollegeData = loader.LoadCollegeData(testCollegeData);

            string res = loader.GetSerializedErrors();

            Assert.IsTrue(res.Length>0);
        }

        [Test]
        public void FileCollegeDataLoaderSerializerxmlTest()
        {
            IMySerializer ser = new MyXmlSerializer();
            CollegeData testCollegeData = new CollegeData(settings, ser);

            FileCollegeDataLoader loader = new FileCollegeDataLoader("SampleData.txt", ser);
            var outputCollegeData = loader.LoadCollegeData(testCollegeData);

            string res = loader.GetSerializedErrors();

            Assert.IsTrue(res.Length > 0);
        }


    }
}
