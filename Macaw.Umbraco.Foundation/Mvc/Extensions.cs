using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Macaw.Umbraco.Foundation.Mvc
{
	public static class Extensions
	{

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
}