using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using EasyNetQ.DI;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;

namespace Neocom.Poc.EasyQ
{
    public class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.EnableDynamicAssemblyCompilation = false;
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            var assms = from assm in Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                where assm.Name.StartsWith("Neocom", StringComparison.InvariantCultureIgnoreCase)
                select AppDomain.CurrentDomain.Load(assm);

            var customAssemblies = assms.ToList();
            var self = Assembly.Load("Neocom.Poc.EasyQ");
            customAssemblies.Add(self);

            InitializeContainer(container, customAssemblies);

            container.RegisterAsEasyNetQContainerFactory();

            //container.Register(() =>
            //{
            //    if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && container.IsVerifying())
            //    {
            //        return new OwinContext().Authentication;
            //    }

            //    return HttpContext.Current.GetOwinContext().Authentication;
            //});

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            var results =
                from result in Analyzer.Analyze(container)
                let disposableController =
                result is DisposableTransientComponentDiagnosticResult &&
                typeof(ApiController).IsAssignableFrom(result.ServiceType)
                where !disposableController
                select result;

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        /// <summary>
        /// Method to initialize Container.
        /// </summary>
        /// <param name="container">Container to be initialized.</param>
        private static void InitializeContainer(Container container, IEnumerable<Assembly> assemblies)
        {
            container.RegisterPackages(assemblies);
        }
    }
}