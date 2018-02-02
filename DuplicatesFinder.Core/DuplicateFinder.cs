using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.Core
{
    public class DuplicateFinder
    {
        private readonly IActorRef _duplicatesFinderActor;

        public DuplicateFinder(ILifetimeScope lifetimeScope)
        {
            var actorSystem = ActorSystem.Create("DuplicateFinderActorSystem");

            var autoFacDependencyResolver = new AutoFacDependencyResolver(lifetimeScope, actorSystem);
            _duplicatesFinderActor = actorSystem.ActorOf(actorSystem.DI().Props<DuplicateFinderOrchestratorActor>(),
                nameof(DuplicateFinderOrchestratorActor));
        }

        public void StartSearchForDuplicates(IResourcesContainer resourcesContainer)
        {
            _duplicatesFinderActor.Tell(new SearchForResourcesMsg(resourcesContainer));
        }
    }
}