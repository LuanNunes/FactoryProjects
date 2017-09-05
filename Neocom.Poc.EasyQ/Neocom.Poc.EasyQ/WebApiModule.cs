using EasyNetQ;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Neocom.Poc.EasyQ
{
    public class WebApiModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            //var serviceBus = RabbitHutch.CreateBus(
            //    "username=guest;password=guest;virtualHost=/;host=localhost");
            //container.Register<IBus>(() => serviceBus, Lifestyle.Singleton);
        }
    }
}