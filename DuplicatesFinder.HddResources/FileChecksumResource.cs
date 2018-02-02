using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.HddResources
{
    public class FileChecksumResource : FileResource
    {
        private readonly string _checksum;

        public FileChecksumResource(IResource resource) : this(resource.Name)
        {
        }

        public FileChecksumResource(string name) : base(name)
        {
            using (var fs = new FileStream(name, FileMode.Open))
            using (var bs = new BufferedStream(fs))
            {
                using (var sha1 = new SHA1Managed())
                {
                    var hash = sha1.ComputeHash(bs);
                    var formatted = new StringBuilder(2 * hash.Length);
                    foreach (var b in hash)
                        formatted.AppendFormat("{0:X2}", b);
                    _checksum = formatted.ToString();
                }
            }
        }

        public override bool Equals(object obj)
        {
            var resource = obj as FileChecksumResource;
            return resource != null &&
                   _checksum == resource._checksum;
        }

        public override int GetHashCode()
        {
            return 1156966681 + EqualityComparer<string>.Default.GetHashCode(_checksum);
        }

        public static bool operator ==(FileChecksumResource resource1, FileChecksumResource resource2)
        {
            return EqualityComparer<FileChecksumResource>.Default.Equals(resource1, resource2);
        }

        public static bool operator !=(FileChecksumResource resource1, FileChecksumResource resource2)
        {
            return !(resource1 == resource2);
        }
    }
}