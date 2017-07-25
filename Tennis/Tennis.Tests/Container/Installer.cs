using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Tennis.DomainLayer.Events;
using Tennis.Helpers.Domain;

namespace Tennis.Tests.Container
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<IHandler<GameStarted>>().ImplementedBy<GameStartedHandler>());
            container.Register(Component.For<IHandler<PointWinned>>().ImplementedBy<PointWinnedHadler>());
            container.Register(Component.For<IHandler<GameWinned>>().ImplementedBy<GameWinnedHandler>());
            container.Register(Component.For<IHandler<SetStarted>>().ImplementedBy<SetStartedHandler>());
            container.Register(Component.For<IHandler<SetWinned>>().ImplementedBy<SetWinnedHandler>());
        }
    }
}
