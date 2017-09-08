using EasyNetQ;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Neocom.Poc.EasyQ
{
    public class WebApiModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IBus>(() => RabbitHutch.CreateBus(
                    "username=guest;password=guest;virtualHost='/';host=neogiglocal"),
                Lifestyle.Singleton);

            //var serviceBus = RabbitHutch.CreateBus(
            //    "username=guest;password=guest;virtualHost=/;host=localhost");
            //container.Register<IBus>(() => serviceBus, Lifestyle.Singleton);
        }
    }
}