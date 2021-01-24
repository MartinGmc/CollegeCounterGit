using CollegeCounter.CollegeData.Implementation;
using CollegeCounter.DataObjects;
using CollegeCounter.Serializer.Implementation;
using CollegeCounter.Serializer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;


namespace CollegeCounter
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var section = configuration.GetSection("Weights");
            List<SubjectWeight> weights = section.Get<List<SubjectWeight>>();

            string filepath = configuration.GetSection("FilePath").Value;
            string outputfileProcess = configuration.GetSection("OutputFileProcessPath").Value;
            string outputFileLoaderErrors = configuration.GetSection("OutputfileLoadErrors").Value;
            string OutputFileOverallPath = configuration.GetSection("OutputFileOverallPath").Value;


            IMySerializer serializer = null;
            foreach (var item in args)
            {
                if (item == "JSON")
                {
                    serializer = new MyJsonSerializer();
                }
            }
            if (serializer == null)
            {
                serializer = new MyXmlSerializer();
            }

            FileCollegeDataLoader loader = new FileCollegeDataLoader(filepath, serializer);
            CollegeDataSettings settings = new CollegeDataSettings() { Subjectweights = weights };
            CollegeData.Implementation.CollegeData data = new CollegeData.Implementation.CollegeData(settings, serializer);

            loader.LoadCollegeData(data);
            File.WriteAllText(Path.ChangeExtension(outputFileLoaderErrors, serializer.GetFileExtension()), loader.GetSerializedErrors());
            data.Process();
            File.WriteAllText(Path.ChangeExtension(outputfileProcess, serializer.GetFileExtension()), data.GetSerializedGroups());
            File.WriteAllText(Path.ChangeExtension(OutputFileOverallPath, serializer.GetFileExtension()), data.GetSerializedOverall());

        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);


        }
    }
}
