using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.Core
{
    public class DuplicatesCheckerActor : ReceiveActor
    {
        private readonly HashSet<IResource> _originalResources = new HashSet<IResource>();

        public DuplicatesCheckerActor()
        {
            Receive<CheckIfDuplicateMsg>(msg =>
            {
                if (_originalResources.Add(msg.Resource)) return;
                if (_originalResources.Any(resource =>
                    string.Equals(resource.Name, msg.Resource.Name, StringComparison.OrdinalIgnoreCase))) return;

                Sender.Tell(new DuplicateFoundMsg(msg.JobId,
                    _originalResources.First(resource => resource.Equals(msg.Resource)), msg.Resource));
            });
        }

        public class CheckIfDuplicateMsg
        {
            public CheckIfDuplicateMsg(Guid jobId, IResource resource)
            {
                JobId = jobId;
                Resource = resource;
            }

            public Guid JobId { get; }
            public IResource Resource { get; }
        }
    }
}