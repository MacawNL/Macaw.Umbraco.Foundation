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
    /// <summary>
    /// Multi node tree picker converter
    /// </summary>
    public class MultiNodeTreePicker : BaseConverter
    {
        /// <summary>
        /// Check converter type
        /// </summary>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return Constants.PropertyEditors.MultiNodeTreePickerAlias.Equals(propertyType.PropertyEditorAlias);
        }

        /// <summary>
        /// Convert to data source
        /// </summary>
        /// <param name="propertyType"></param>
        /// <param name="source"></param>
        /// <param name="preview"></param>
        /// <returns></returns>
        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            var ret = new List<DynamicModel>(); //return value

            if(source != null && !string.IsNullOrEmpty(source.ToString()))
            {
                foreach (var item in source.ToString().Split(',')) // source contains a comma seperate value
                {
                    int id = 0;
                    if(int.TryParse(item, out id))
                    {
                        ret.Add(Repository.FindById(id));
                    }
                }
            }
            return ret;
        }
    }
}
