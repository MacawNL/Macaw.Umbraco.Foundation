using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
	public abstract class BaseConverter : PropertyValueConverterBase
    {
		protected ISiteRepository Repository;
		protected UmbracoHelper Helper;

		public BaseConverter() 
        {
			Initialize(DependencyResolver.Current.GetService<ISiteRepository>());
        }

		public BaseConverter(ISiteRepository rep)
        {
            Initialize(rep);
        }

        protected void Initialize(ISiteRepository rep)
        {
			Repository = rep;
        }
	}
}
