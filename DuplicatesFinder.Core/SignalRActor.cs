using Akka.Actor;
using Microsoft.AspNetCore.SignalR;

namespace DuplicatesFinder.Core
{
    public class SignalRActor : ReceiveActor
    {
        private readonly IHubContext<DuplicatesFinderHub> _hub;

        public class DuplicatesFinderHub : Hub
        {
        }

        public SignalRActor(IHubContext<DuplicatesFinderHub> hubContext)
        {
            _hub = hubContext;
            Receive<DuplicateFoundMsg>(duplicate =>
            {
                _hub.Clients.All.InvokeAsync("DuplicateFound", duplicate);
            });
        }
    }
}