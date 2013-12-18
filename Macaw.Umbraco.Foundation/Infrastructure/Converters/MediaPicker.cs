using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Dynamics;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class MediaPicker : BaseConverter
    {
        public MediaPicker()
            : base ()
        {
        }

        public override bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            return Guid.Parse("ead69342-f06d-4253-83ac-28000225583b").Equals(propertyEditorId);
        }

        public override Attempt<object> ConvertPropertyValue(object value)
        {
			int val;
			if (int.TryParse(value.ToString(), out val))
			{
				var media = Repository.FindMediaById(val);
				return new Attempt<object>(true, media);
			}
			else
				return new Attempt<object>(true, new DynamicNullMedia());
        }
    }
}
