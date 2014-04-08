using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Umbraco.Core;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Macaw.Umbraco.Foundation.Infrastructure.Converters.Models;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class MacroContainer : BaseConverter, IConverter
	{
		public override bool IsConverter(PublishedPropertyType propertyType)
		{
            return IsConverter(propertyType.PropertyEditorAlias);
		}

        public bool IsConverter(string editoralias)
        {
            return Constants.PropertyEditors.MacroContainerAlias.Equals(editoralias);
        }

		public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
		{
		    return ConvertDataToSource(source);
		}

	    public object ConvertDataToSource(object source)
	    {
            string content = source.ToString();
            if (!string.IsNullOrWhiteSpace(content) && UmbracoContext.Current != null && UmbracoContext.Current.PageId.HasValue)
            {
                var macros = Regex.Matches(content, "(\\<\\?UMBRACO_MACRO.+?(\\/>))");
                var ret = new List<DynamicMacroModelHtmlProxy>();
                foreach (Match match in macros) 
                {
                    var parameters = Regex.Matches(match.Value, "(\\w+)=(\"[^<>\"]*\"|\'[^<>\']*\'|\\w+)").Cast<Match>().ToList();
                    var alias = parameters.Where(val => val.Groups[1].Value.Equals("macroAlias"))
                        .Select(val => val.Groups[2].Value.Replace("\"", string.Empty).Replace("'", string.Empty)).FirstOrDefault();

                    var values = parameters.Where(val => !val.Groups[1].Value.Equals("macroAlias"))
                        .ToDictionary(val => val.Groups[1].Value, val => val.Groups[2].Value.Replace("\"", string.Empty).Replace("'", string.Empty) as object);

                    var macroProxy = new DynamicMacroModelHtmlProxy(Repository.FindMacroByAlias(alias, UmbracoContext.Current.PageId.Value, values), 
                        UmbracoContext.Current.PageId.Value, match.Value);

                    ret.Add(macroProxy);
                }

                return ret;
            }
            else
            {
                return DynamicNull.Null;
            }
	    }
	}
}
