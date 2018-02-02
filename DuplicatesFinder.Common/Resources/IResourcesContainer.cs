using System.Collections.Generic;

namespace DuplicatesFinder.Common.Resources
{
    public interface IResourcesContainer
    {
        IEnumerable<IResource> GetResources();
    }
}