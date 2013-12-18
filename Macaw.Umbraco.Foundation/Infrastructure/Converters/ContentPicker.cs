using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    public class ContentPicker : BaseConverter, IPropertyEditorValueConverter
    {
		public ContentPicker()
			: base()
		{ 
		}

        public ContentPicker(ISiteRepository rep)
			: base(rep)
		{
		}

        public override bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            return Guid.Parse("158aa029-24ed-4948-939e-c3da209e5fba").Equals(propertyEditorId);
        }

        public override Attempt<object> ConvertPropertyValue(object value)
        {
			var content = Repository.FindById(int.Parse(value.ToString()));
            return new Attempt<object>(true, content);
        }
    }
}
