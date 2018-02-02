using Akka.Actor;
using Akka.TestKit.NUnit;
using DuplicatesFinder.Common.Resources;
using DuplicatesFinder.Core;
using FluentAssertions;
using NUnit.Framework;

namespace DuplicateFinder.Core.Tests
{
    [TestFixture]
    public class DuplicateFinder : TestKit
    {
        [Test]
        public void DuplicateFinder_should_return_proper_duplicates()
        {
            // given
            IResource[] originalResources =
            {
                new TestResource("id1", "test1", "details1"),
                new TestResource("id2", "test2", "details2"),
                new TestResource("id3", "test3", "details3"),
                new TestResource("id4", "test4", "details4"),
                new TestResource("id5", "test5", "details5"),
                new TestResource("id6", "test6", "details6"),
                new TestResource("id7", "test7", "details7"),
                new TestResource("id8", "test8", "details8")
            };

            IResource[] duplicatedResources =
            {
                new TestResource("id1", "testduplicate1", "details1"),
                new TestResource("id3", "testduplicate3", "details3"),
                new TestResource("id8", "testduplicate8", "details8")
            };

            var actor = Sys.ActorOf(Props.Create(() => new DuplicatesCheckerActor()));
            foreach (var originalResource in originalResources)
                actor.Tell(new DuplicatesCheckerActor.CheckIfDuplicateMsg(originalResource));

            // when
            foreach (var duplicatedResource in duplicatedResources)
                actor.Tell(new DuplicatesCheckerActor.CheckIfDuplicateMsg(duplicatedResource));

            // then
            var foundMsg = ExpectMsg<DuplicateFoundMsg>();
            foundMsg.Original.Name.ShouldBeEquivalentTo(originalResources[0].Name);
            foundMsg.Duplicate.Name.ShouldBeEquivalentTo(duplicatedResources[0].Name);

            foundMsg = ExpectMsg<DuplicateFoundMsg>();
            foundMsg.Original.Name.ShouldBeEquivalentTo(originalResources[2].Name);
            foundMsg.Duplicate.Name.ShouldBeEquivalentTo(duplicatedResources[1].Name);

            foundMsg = ExpectMsg<DuplicateFoundMsg>();
            foundMsg.Original.Name.ShouldBeEquivalentTo(originalResources[7].Name);
            foundMsg.Duplicate.Name.ShouldBeEquivalentTo(duplicatedResources[2].Name);
        }
    }
}