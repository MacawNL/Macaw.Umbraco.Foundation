using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core;

namespace Macaw.Umbraco.Foundation.Mvc
{
	public static class Extensions
	{
        public static MvcHtmlString ToJson(this HtmlHelper html, object obj)
        {
            var ret = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return MvcHtmlString.Create(ret);
        }

        public static MvcHtmlString ToJson(this HtmlHelper html, IPublishedContent content, string[] properties = null, bool includeHiddenItems = true)
        {
            var ret = string.Empty;
			if ((bool)content.GetProperty(Constants.Conventions.Content.NaviHide).Value != includeHiddenItems)
            {
                ret = Newtonsoft.Json.JsonConvert.SerializeObject(ExtensionHelpers.ToDynamic(content, properties));
            }

            return MvcHtmlString.Create(ret);
        }

        public static MvcHtmlString ToJson(this HtmlHelper html, IEnumerable<IPublishedContent> collection, string[] properties = null, bool includeHiddenItems = true)
        {
            string ret = "undefined";
            if(!includeHiddenItems) //not using IsVisible() here because it's not easy to mock for testing...
				ret = Newtonsoft.Json.JsonConvert.SerializeObject(ExtensionHelpers.ToDynamic(collection.Where(p => (bool)p.GetProperty(Constants.Conventions.Content.NaviHide).Value), properties));
            else
                ret = Newtonsoft.Json.JsonConvert.SerializeObject(ExtensionHelpers.ToDynamic(collection, properties));

            return MvcHtmlString.Create(ret);
        }

		public static string NoHtmlString(this HtmlHelper helper, object value, int maxChars)
		{
			var noHtml = NoHtmlString(helper, value);
			var substring = noHtml.Substring(0, noHtml.Length < maxChars ? noHtml.Length : maxChars);
			int i = maxChars;
			while (i < noHtml.Length - 1 && noHtml[i] != ' ' && noHtml[i] != '.')
			{
				substring += noHtml[i];
				i++;
			}

			if (substring.Length != noHtml.Length)
				substring += "...";

			return substring;
		}

		public static string NoHtmlString(this HtmlHelper helper, object value)
		{
			if (value != null)
			{
				//initialize regular expressions
				string htmltag = "</?\\w+((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?>";
				string emptytags = "<(\\w+)((\\s+\\w+(\\s*=\\s*(?:\".*?\"|'.*?'|[^'\">\\s]+))?)+\\s*|\\s*)/?></\\1>";

				var ret = Regex.Replace(value.ToString(), htmltag, string.Empty, RegexOptions.IgnoreCase);
				return Regex.Replace(ret, emptytags, string.Empty, RegexOptions.IgnoreCase);
			}
			else
				return string.Empty;
		}
	}

    internal class ExtensionHelpers
    {
        internal static dynamic ToDynamic(IPublishedContent content, string[] aliases)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            if (aliases != null)
            {
                foreach (var prop in content.Properties.Where(p => aliases.Contains(p.PropertyTypeAlias)))
					expando.Add(prop.PropertyTypeAlias, prop.Value);
            }
            else
            {
                foreach (var prop in content.Properties)
					expando.Add(prop.PropertyTypeAlias, prop.Value);
            }

            return expando as ExpandoObject;
        }

        internal static IEnumerable<dynamic> ToDynamic(IEnumerable<IPublishedContent> collection, string[] aliases)
        {
            foreach (var item in collection)
                yield return ToDynamic(item, aliases);
        }
    }

}