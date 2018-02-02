using System.Collections.Generic;
using System.IO;
using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.HddResources
{
    public class FileSizeResource : FileResource
    {
        private readonly long _size;

        public FileSizeResource(IResource resource) : this(resource.Name)
        {
        }

        public FileSizeResource(string name) : base(name)
        {
            _size = new FileInfo(name).Length;
        }

        public override bool Equals(object obj)
        {
            var resource = obj as FileSizeResource;
            return resource != null &&
                   _size == resource._size;
        }

        public override int GetHashCode()
        {
            return 1957475591 + _size.GetHashCode();
        }

        public static bool operator ==(FileSizeResource resource1, FileSizeResource resource2)
        {
            return EqualityComparer<FileSizeResource>.Default.Equals(resource1, resource2);
        }

        public static bool operator !=(FileSizeResource resource1, FileSizeResource resource2)
        {
            return !(resource1 == resource2);
        }
    }
}