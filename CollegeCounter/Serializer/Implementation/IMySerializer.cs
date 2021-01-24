

namespace CollegeCounter.Serializer.Implementation
{
    public interface IMySerializer
    {
        abstract string SerializeObject(object input);

        abstract string GetFileExtension();
    }
}
