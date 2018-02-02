using System;
using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.Core
{
    public class DuplicateFoundMsg
    {
        public DuplicateFoundMsg(Guid jobId, IResource original, IResource duplicate)
        {
            JobId = jobId;
            Original = original;
            Duplicate = duplicate;
        }

        public Guid JobId { get; }
        public IResource Original { get; }
        public IResource Duplicate { get; }
    }
}