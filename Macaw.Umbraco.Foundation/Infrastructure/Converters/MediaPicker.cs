using Macaw.Umbraco.Foundation.Core.Models;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class MediaPicker : BaseConverter, IConverter
	{
		public override bool IsConverter(PublishedPropertyType propertyType)
		{
            return IsConverter(propertyType.PropertyEditorAlias);
		}

        public bool IsConverter(string editoralias)
        {
            return Constants.PropertyEditors.MediaPickerAlias.Equals(editoralias);
        }

		public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
		{
		    return ConvertDataToSource(source);
		}

	    public object ConvertDataToSource(object source)
	    {
            int val;
            if (int.TryParse(source.ToString(), out val))
            {
                var media = Repository.FindMediaById(val);

                if (media != null)
                    return media;
            }

            return DynamicNullMedia.Null;
	    }
	}
}
