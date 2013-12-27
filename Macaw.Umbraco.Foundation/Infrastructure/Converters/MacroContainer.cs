using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class MacroContainer : BaseConverter
    {
		public override bool IsConverter(PublishedPropertyType propertyType)
		{
			return Constants.PropertyEditors.MacroContainerAlias.Equals(propertyType.PropertyEditorAlias);
		}

		public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
		{
			string content = source.ToString();
			if (!string.IsNullOrWhiteSpace(content) &&  UmbracoContext.Current != null && UmbracoContext.Current.PageId.HasValue)
			{
				//todo: seems like we using legacy code here..
				return new System.Web.HtmlString(umbraco.library.RenderMacroContent(content, UmbracoContext.Current.PageId.Value));
			}
			else
			{
				return DynamicNull.Null;
			}
		}
    }
}
