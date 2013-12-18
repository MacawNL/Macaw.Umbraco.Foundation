using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class RelatedLinks : BaseConverter, IPropertyEditorValueConverter
    {
		public RelatedLinks() 
			: base ()
        {
        }

        public RelatedLinks(ISiteRepository repository)
			: base(repository)
		{
		}

        public override bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
			return Guid.Parse("71b8ad1a-8dc2-425c-b6b8-faa158075e63").Equals(propertyEditorId);
        }

        public override Attempt<object> ConvertPropertyValue(object value)
        {
			var elem = System.Xml.Linq.XElement.Parse(value.ToString());
			var serializer = new XmlSerializer(typeof(Link));

			var ret = new List<IUrlModel>();
			foreach (var link in elem.Elements())
			{
				var typedLink = serializer.Deserialize(link.CreateReader()) as Link;
				int id;
				if (typedLink.Type.ToLower().Equals("internal") && int.TryParse(typedLink.Url, out id))
				{
					typedLink.Url = Repository.FindById(id).Url;
				}

				ret.Add(typedLink);
			}

			return new Attempt<object>(true, ret.AsEnumerable());
        }
    }


	[XmlType("link")]
	public class Link : IUrlModel, System.Web.IHtmlString
	{
		[XmlAttribute("title")]
		public string Title { get; set; }

		[XmlAttribute("link")]
		public string Url { get; set; }

		[XmlAttribute("newwindow")]
		public bool NewWindow { get; set; }

		[XmlAttribute("type")]
		public string Type { get; set; }

		public override string ToString()
		{
			return string.Format("<a href=\"{1}\" target=\"{2}\">{0}</a>",
				Title,
				Url,
				NewWindow ? "_blank" : "_self");
		}

		public string ToHtmlString()
		{
			return new System.Web.HtmlString(ToString()).ToHtmlString();
		}

		public bool IsNull()
		{
			return false;
		}
	}
}
