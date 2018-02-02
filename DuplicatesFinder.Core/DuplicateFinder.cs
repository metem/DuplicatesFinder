using System;
using Akka.Actor;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using DuplicatesFinder.Common.Resources;
using Microsoft.AspNetCore.SignalR;

namespace DuplicatesFinder.Core
{
    public class DuplicateFinder
    {
        private readonly IActorRef _duplicateFinderOrchestratorActor;

        public DuplicateFinder(ILifetimeScope lifetimeScope)
        {
            var actorSystem = ActorSystem.Create("DuplicateFinderActorSystem");

            var autoFacDependencyResolver = new AutoFacDependencyResolver(lifetimeScope, actorSystem);
            _duplicateFinderOrchestratorActor = actorSystem.ActorOf(actorSystem.DI().Props<DuplicateFinderOrchestratorActor>(),
                nameof(DuplicateFinderOrchestratorActor));
        }

        public Guid StartSearchForDuplicates(IResourcesContainer resourcesContainer)
        {
            var jobId = Guid.NewGuid();
            _duplicateFinderOrchestratorActor.Tell(new SearchForResourcesMsg(jobId, resourcesContainer));
            return jobId;
        }
    }
}