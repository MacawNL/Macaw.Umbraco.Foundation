using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Umbraco.Foundation.Core
{
	public interface IServiceLocator
	{
		T GetInstance<T>();
		object GetInstance(Type type);
	}

	public static class ServiceLocator
	{
		private static IServiceLocator _current;
		public static IServiceLocator Current
		{
			get
			{
				return _current;
			}
		}

		public static void SetServiceLocator(Func<IServiceLocator> create)
		{
			if (create == null) throw new ArgumentNullException("create");
			_current = create();
		}
	}
}
