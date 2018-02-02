using System.Collections.Generic;
using DuplicatesFinder.Common.Resources;

namespace DuplicateFinder.Core.Tests
{
    public class TestResource : IResource
    {
        public TestResource(string id, string name, string details)
        {
            Id = id;
            Name = name;
            Details = details;
        }

        public string Id { get; }
        public string Details { get; }
        public string Name { get; }

        public override bool Equals(object obj)
        {
            var resource = obj as TestResource;
            return resource != null &&
                   Id == resource.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
        }

        public static bool operator ==(TestResource resource1, TestResource resource2)
        {
            return EqualityComparer<TestResource>.Default.Equals(resource1, resource2);
        }

        public static bool operator !=(TestResource resource1, TestResource resource2)
        {
            return !(resource1 == resource2);
        }
    }
}