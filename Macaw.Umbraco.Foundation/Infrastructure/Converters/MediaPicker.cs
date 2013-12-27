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
				return media;
			}
			else
				return DynamicNullMedia.Null;
		}

		//public MediaPicker()
		//	: base()
		//{
		//}

		//public override bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
		//{
		//	return Guid.Parse("ead69342-f06d-4253-83ac-28000225583b").Equals(propertyEditorId);
		//}

		//public override Attempt<object> ConvertPropertyValue(object value)
		//{
		//	int val;
		//	if (int.TryParse(value.ToString(), out val))
		//	{
		//		var media = Repository.FindMediaById(val);
		//		return new Attempt<object>(true, media);
		//	}
		//	else
		//		return new Attempt<object>(true, DynamicNullMedia.Null);
		//}
	}
}
