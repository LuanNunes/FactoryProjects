using EasyNetQ;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Neocom.Poc.EasyQ.Receiver
{
    public class WebApiModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IBus>(() => RabbitHutch.CreateBus(
                "username=webapp;password=webapp;virtualHost=/;host=neogigdev"), Lifestyle.Singleton);
        }
    }
}