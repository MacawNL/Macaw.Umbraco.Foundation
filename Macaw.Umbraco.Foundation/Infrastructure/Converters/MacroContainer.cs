using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Convert data to source
        /// </summary>
        /// <param name="propertyType"></param>
        /// <param name="source"></param>
        /// <param name="preview"></param>
        /// <returns></returns>
        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            string content = source.ToString();
            if (!string.IsNullOrWhiteSpace(content) && UmbracoContext.Current != null && UmbracoContext.Current.PageId.HasValue)
            {
                MatchCollection macros = Regex.Matches(content, "(\\<\\?UMBRACO_MACRO.+?(\\ />))");
                List<HtmlString> ret = new List<HtmlString>();
                foreach (Match macro in macros) ////todo: seems like we using legacy code here..
                    ret.Add(new HtmlString(umbraco.library.RenderMacroContent(macro.Value, UmbracoContext.Current.PageId.Value)));

                return ret;
            }
            else
            {
                return DynamicNull.Null;
            }
        }

	}
}
