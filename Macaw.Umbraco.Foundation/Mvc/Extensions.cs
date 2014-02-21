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
using Macaw.Umbraco.Foundation.Core.Models;
using Macaw.Umbraco.Foundation.Core;

namespace Macaw.Umbraco.Foundation.Mvc
{
	public static class Extensions
	{
		/// <summary>
		/// Translate the string by using the string itself as the key.
		/// use the querystring ?debugtranslation=true to show all translation keys on a rendered page.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Translate(this string str)
		{
			return str.Translate(str);
		}

		/// <summary>
		/// Translate the string by using the given key
		/// use the querystring ?debugtranslation=true to show all translation keys on a rendered page.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string Translate(this string str, string key)
		{
			//show keys instead of translation..
			if (HttpContext.Current != null && !HttpContext.Current.Request.QueryString["debugtranslation"].IsNullOrWhiteSpace())
			{
				return string.Format("#{0}",key);
			}

			var repo = DependencyResolver.Current.GetService<ISiteRepository>();
			var ret = repo.Translate(key);

			if (ret.IsNullOrWhiteSpace())
				return str;
			else
				return ret;

			//todo: auto add items into the dictionary when a specific boolean is set in the webconfig.
		}
		/// <summary>
		/// Turn your IPublished content into a class that inherits from dynamic model
		/// The class must have a 2 parameter constructur (IPublishedContent source, ISiteRepository repository)
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content"></param>
		/// <returns></returns>
		public static T As<T>(this IPublishedContent content) where T : DynamicModel
		{
			ISiteRepository repo;

			var dyn = content as DynamicModel;
			if (dyn != null) //keep using already defined repository.
				repo = dyn.Repository;
			else //get repository from service locator.
				repo = DependencyResolver.Current.GetService<ISiteRepository>();
				
			return (T)Activator.CreateInstance(typeof(T), content, repo);
		}

		public static string LimitLength(this string source, int maxLength)
		{
			if (source.Length <= maxLength)
			{
				return source;
			}

			return source.Substring(0, maxLength);
		}

        public static MvcHtmlString ToJson(this HtmlHelper html, object obj)
        {
            if (obj is IPublishedContent)
            {
                return ToJson(html, (IPublishedContent)obj);
            }
            else if (obj is IEnumerable<IPublishedContent>)
            {
                return ToJson(html, (IEnumerable<IPublishedContent>)obj);
            }
            else
            {
                var ret = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                return MvcHtmlString.Create(ret);
            }
        }

		public static MvcHtmlString ToJson(this HtmlHelper html, IPublishedContent content, string[] properties = null, bool includeHiddenItems = true)
		{
			var ret = string.Empty;
			if (!(bool)content.GetProperty(Constants.Conventions.Content.NaviHide).Value || includeHiddenItems)
			{
				ret = Newtonsoft.Json.JsonConvert.SerializeObject(ExtensionHelpers.ToDynamic(content, properties));
			}

			return MvcHtmlString.Create(ret);
		}

		public static MvcHtmlString ToJson(this HtmlHelper html, IEnumerable<IPublishedContent> collection, string[] properties = null, bool includeHiddenItems = true)
		{
			string ret = "undefined";
			if (!includeHiddenItems) //not using IsVisible() here because it's not easy to mock for testing...
				ret = Newtonsoft.Json.JsonConvert.SerializeObject(ExtensionHelpers.ToDynamic(collection.Where(p => !(bool)p.GetProperty(Constants.Conventions.Content.NaviHide).Value), properties));
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
				{
					if (prop.Value is HtmlString || prop.Value is DynamicMediaModel) //htmlstring & DynamicMediaModel can not be serialized with Newtonsoft json..
						expando.Add(prop.PropertyTypeAlias, prop.Value.ToString());
					else
						expando.Add(prop.PropertyTypeAlias, prop.Value);
				}
			}
			else
			{
				foreach (var prop in content.Properties)
				{
					if (prop.Value is HtmlString || prop.Value is DynamicMediaModel) //htmlstring & DynamicMediaModel can not be serialized with Newtonsoft json..
						expando.Add(prop.PropertyTypeAlias, prop.Value.ToString());
					else
						expando.Add(prop.PropertyTypeAlias, prop.Value);
				}
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