using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models.PublishedContent;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
	public class MediaPicker : BaseConverter
	{
		public override bool IsConverter(PublishedPropertyType propertyType)
		{
			return Constants.PropertyEditors.MediaPickerAlias.Equals(propertyType.PropertyEditorAlias);
		}

		public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
		{
			int val;
			if (int.TryParse(source.ToString(), out val))
			{
				var media = Repository.FindMediaById(val);

				if(media != null)
					return media;
			}
			
			return DynamicNullMedia.Null;
		}
	}
}
