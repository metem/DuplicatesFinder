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
                    Sender.Tell(new ResourceFoundMsg(resource));
            });
        }

        public class ResourceFoundMsg
        {
            public ResourceFoundMsg(IResource resource)
            {
                Resource = resource;
            }

            public IResource Resource { get; }
        }
    }
}