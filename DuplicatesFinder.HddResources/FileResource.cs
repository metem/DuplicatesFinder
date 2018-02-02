using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.HddResources
{
    public class FileResource : IResource
    {
        public FileResource(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}