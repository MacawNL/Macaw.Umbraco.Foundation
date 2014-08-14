using System;
using System.Collections.Generic;
using System.Dynamic;
using Macaw.Umbraco.Foundation.Core.Models;
using System.Web;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters.Models
{
    /// <summary>
    /// Proxy Model for DynamicMacroModel implementing IHtmlString to render a macro while calling IHtmlString
    /// </summary>
    public class DynamicMacroModelHtmlProxy : DynamicMacroModel, IHtmlString
    {
        public DynamicMacroModelHtmlProxy(DynamicMacroModel source,
            int pageId, string macroValue) : base (source.Macro, source.Source, source.Repository)
        {
            _macroValue = macroValue;
            _pageId = pageId;
        }

        private readonly string _macroValue;
        private readonly int _pageId;

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(_macroValue))
                return umbraco.library.RenderMacroContent(_macroValue,
                    _pageId);

            return Macro.Alias; //default;
        }

        public string ToHtmlString()
        {
            return new HtmlString(ToString()).ToString();
        }
    }
}
