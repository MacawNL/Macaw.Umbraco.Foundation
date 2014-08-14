using Autofac;
using Autofac.Integration.Mvc;
using Macaw.Umbraco.Foundation.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace Macaw.Umbraco.Foundation.Test
{
    public class BaseTestClass
    {
        [TestInitialize]
        public void Initialize()
        {

            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(Macaw.Umbraco.Foundation.Controllers.DynamicBaseController).Assembly);
            builder.RegisterControllers(System.Reflection.Assembly.GetExecutingAssembly());

            var mSite = new Mock<ISiteRepository>();
            builder.Register(s => mSite.Object)
                .As<ISiteRepository>();

            var container = builder.Build();
            var lifetimeScopeProvider = new StubLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, lifetimeScopeProvider);
            DependencyResolver.SetResolver(resolver);
            // Now you can use DependencyResolver.Current in
            // tests without getting the web request lifetime exception.
        }
    }
}
