using CollegeCounter.Serializer.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CollegeCounter.Serializer.Interface
{
    public class MyJsonSerializer : IMySerializer
    {
        public string GetFileExtension()
        {
            return ".json";
        }

        public string SerializeObject(object input)
        {
            string jsonString = JsonSerializer.Serialize(input);
            return jsonString;
        }
    }
}
