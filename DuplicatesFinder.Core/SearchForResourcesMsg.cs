using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.Core
{
    public class SearchForResourcesMsg
    {
        public SearchForResourcesMsg(IResourcesContainer container)
        {
            Container = container;
        }

        public IResourcesContainer Container { get; }
    }
}