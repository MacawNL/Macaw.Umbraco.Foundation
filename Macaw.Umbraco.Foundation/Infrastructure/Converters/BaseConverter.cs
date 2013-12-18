using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
	[Obsolete("http://issues.umbraco.org/issue/U4-2828")]
    public abstract class BaseConverter : IPropertyEditorValueConverter
    {
		protected ISiteRepository Repository;
		protected UmbracoHelper Helper;

		[Obsolete("use di")]
		public BaseConverter() 
        {
			var Context = UmbracoContext.Current;
			Helper = new UmbracoHelper(Context);

            //todo: use dependency injection..
			Initialize(new Macaw.Umbraco.Foundation.Infrastructure.SiteRepository(
                ApplicationContext.Current.Services.ContentService,  
                Helper));
        }

		public BaseConverter(ISiteRepository rep)
        {
            Initialize(rep);
        }

        protected void Initialize(ISiteRepository rep)
        {
            Repository = rep;
        }

		public abstract bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias);

		public abstract Attempt<object> ConvertPropertyValue(object value);
    }
}
