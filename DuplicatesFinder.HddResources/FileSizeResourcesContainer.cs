using System.Collections.Generic;
using System.IO;
using System.Linq;
using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.HddResources
{
    public class FileSizeResourcesContainer : IResourcesContainer
    {
        private readonly string _path;

        public FileSizeResourcesContainer(string path)
        {
            _path = path;
        }

        public IEnumerable<IResource> GetResources()
        {
            return Directory.GetFiles(_path).Select(fileName => new FileResource(fileName));
        }
    }
}