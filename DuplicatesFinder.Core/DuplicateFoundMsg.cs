using DuplicatesFinder.Common.Resources;

namespace DuplicatesFinder.Core
{
    public class DuplicateFoundMsg
    {
        public DuplicateFoundMsg(IResource original, IResource duplicate)
        {
            Original = original;
            Duplicate = duplicate;
        }

        public IResource Original { get; }
        public IResource Duplicate { get; }
    }
}