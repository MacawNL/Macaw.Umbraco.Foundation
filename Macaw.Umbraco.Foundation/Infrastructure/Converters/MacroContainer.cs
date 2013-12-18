using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Dynamics;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class MacroContainer : BaseConverter
    {
		public MacroContainer()
			: base() { }

		private string _propertyAlias;

        public override bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
			var ret = Guid.Parse("474fcff8-9d2d-11de-abc6-ad7a56d89593").Equals(propertyEditorId);
			if(ret)
			{
				_propertyAlias = propertyTypeAlias;
			}

			return ret;
        }

        public override Attempt<object> ConvertPropertyValue(object value)
        {
            if (UmbracoContext.Current != null && UmbracoContext.Current.PageId.HasValue)
            {
				return new Attempt<object>(true, Helper.Field(Repository.FindById(UmbracoContext.Current.PageId.Value), _propertyAlias));
            }
            else
            {
				return new Attempt<object>(true, new DynamicNull());
            }
        }
    }
}
