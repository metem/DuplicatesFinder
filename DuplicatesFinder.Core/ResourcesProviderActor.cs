using System;
using Akka.Actor;
using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.Core
{
    public class ResourcesProviderActor : ReceiveActor
    {
        public ResourcesProviderActor()
        {
            Receive<SearchForResourcesMsg>(msg =>
            {
                foreach (var resource in msg.Container.GetResources())
                    Sender.Tell(new ResourceFoundMsg(msg.JobId, resource));
            });
        }

        public class ResourceFoundMsg
        {
            public ResourceFoundMsg(Guid jobId, IResource resource)
            {
                JobId = jobId;
                Resource = resource;
            }

            public Guid JobId { get; }
            public IResource Resource { get; }
        }
    }
}