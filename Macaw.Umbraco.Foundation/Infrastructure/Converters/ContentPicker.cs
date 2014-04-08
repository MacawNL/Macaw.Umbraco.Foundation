using System;
using Umbraco.Core;
using Umbraco.Core.Dynamics;
using Umbraco.Core.Models.PublishedContent;


namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class ContentPicker : BaseConverter, IConverter
    {
		public override bool IsConverter(PublishedPropertyType propertyType)
		{
		    return IsConverter(propertyType.PropertyEditorAlias);
		}

        public bool IsConverter(string editoralias)
        {
            return Constants.PropertyEditors.ContentPickerAlias.Equals(editoralias);
        }

		public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
		{
		    return ConvertDataToSource(source);
		}

        public object ConvertDataToSource(object source)
        {
            if (source != null && !source.ToString().IsNullOrWhiteSpace())
            {
                var content = Repository.FindById(Convert.ToInt32(source));

                if (content != null)
                    return content;
            }

            return DynamicNull.Null;
        }
    }
}
