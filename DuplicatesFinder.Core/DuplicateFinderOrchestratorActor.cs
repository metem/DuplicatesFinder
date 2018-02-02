using Akka.Actor;
using Akka.DI.Core;
using DuplicatesFinder.HddResources;

namespace DuplicatesFinder.Core
{
    public class DuplicateFinderOrchestratorActor : ReceiveActor
    {
        private readonly IActorRef _firstStageDuplicateChecker;
        private readonly IActorRef _resourcesProvider;
        private readonly IActorRef _secondStageDuplicateChecker;
        private IActorRef _signalRActor;

        public DuplicateFinderOrchestratorActor()
        {
            _signalRActor = Context.ActorOf(Context.DI().Props<SignalRActor>(),
                nameof(SignalRActor));
            _resourcesProvider = Context.ActorOf(Context.System.DI().Props<ResourcesProviderActor>(),
                nameof(ResourcesProviderActor));
            _firstStageDuplicateChecker = Context.ActorOf(Context.System.DI().Props<DuplicatesCheckerActor>(),
                $"first{nameof(DuplicatesCheckerActor)}");
            _secondStageDuplicateChecker = Context.ActorOf(Context.System.DI().Props<DuplicatesCheckerActor>(),
                $"second{nameof(DuplicatesCheckerActor)}");

            Receive<SearchForResourcesMsg>(msg => _resourcesProvider.Tell(msg));

            Receive<ResourcesProviderActor.ResourceFoundMsg>(msg =>
                _firstStageDuplicateChecker.Tell(
                    new DuplicatesCheckerActor.CheckIfDuplicateMsg(new FileSizeResource(msg.Resource))));

            Receive<DuplicateFoundMsg>(msg =>
            {
                if (msg.Duplicate is FileSizeResource)
                {
                    _secondStageDuplicateChecker.Tell(
                        new DuplicatesCheckerActor.CheckIfDuplicateMsg(new FileChecksumResource(msg.Original)));
                    _secondStageDuplicateChecker.Tell(
                        new DuplicatesCheckerActor.CheckIfDuplicateMsg(new FileChecksumResource(msg.Duplicate)));
                }
                else
                {
                    _signalRActor.Tell(msg);
                }
            });
        }
    }
}