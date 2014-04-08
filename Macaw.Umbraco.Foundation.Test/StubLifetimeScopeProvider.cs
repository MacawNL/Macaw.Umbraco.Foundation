using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Umbraco.Foundation.Test
{
    public class StubLifetimeScopeProvider : ILifetimeScopeProvider
    {
        ILifetimeScope _lifetimeScope;
        readonly ILifetimeScope _container;

        public StubLifetimeScopeProvider(ILifetimeScope container)
        {
            _container = container;
        }

        public ILifetimeScope ApplicationContainer
        {
            get { return _container; }
        }

        public ILifetimeScope GetLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return _lifetimeScope ?? (_lifetimeScope = BuildLifetimeScope(configurationAction));
        }

        public void EndLifetimeScope()
        {
            if (_lifetimeScope != null)
                _lifetimeScope.Dispose();
        }

        ILifetimeScope BuildLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return (configurationAction == null)
                       ? _container.BeginLifetimeScope("AutofacWebRequest")
                       : _container.BeginLifetimeScope("AutofacWebRequest", configurationAction);
        }
    }
}
