using Autofac;

namespace DuplicatesFinder.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SignalRActor>().SingleInstance();
            builder.RegisterType<DuplicateFinder>().SingleInstance();
            builder.RegisterType<DuplicateFinderOrchestratorActor>();
            builder.RegisterType<DuplicatesCheckerActor>();
            builder.RegisterType<ResourcesProviderActor>();
        }
    }
}