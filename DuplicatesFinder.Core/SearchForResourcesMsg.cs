using System;
using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.Core
{
    public class SearchForResourcesMsg
    {
        public SearchForResourcesMsg(Guid jobId, IResourcesContainer container)
        {
            JobId = jobId;
            Container = container;
        }

        public Guid JobId { get; }
        public IResourcesContainer Container { get; }
    }
}