using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class ContentPicker : BaseConverter
    {
		public override bool IsConverter(PublishedPropertyType propertyType)
		{
			return Constants.PropertyEditors.ContentPickerAlias.Equals(propertyType.PropertyEditorAlias);
		}

		public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
		{
			var content = Repository.FindById(Convert.ToInt32(source));
			return content;
		}
    }
}
