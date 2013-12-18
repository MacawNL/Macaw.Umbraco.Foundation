using Autofac;
using Macaw.Umbraco.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.DependencyResolution
{
	public class AutofacServiceLocator : IServiceLocator
	{
		//private readonly IContainer _container;

		public AutofacServiceLocator()
		{
			//_container = container;
		}

		public T GetInstance<T>()
		{
			try
			{
				return DependencyResolver.Current.GetService<T>();
			}
			catch (InvalidOperationException ex)
			{
				throw new Exception("Dependency resolution failed", ex);
			}
			catch (Exception)
			{
				return default(T);
			}
		}

		public object GetInstance(Type type)
		{
			try
			{
				return DependencyResolver.Current.GetService(type);
			}
			catch (InvalidOperationException ex)
			{
				throw new Exception("Dependency resolution failed", ex);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
