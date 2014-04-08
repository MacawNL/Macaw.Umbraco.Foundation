using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Umbraco.Foundation.Infrastructure.Converters
{
    /// <summary>
    /// Used for a Converter construction for macro parameters
    /// </summary>
    public interface IConverter
    {
        object ConvertDataToSource(object source);
        bool IsConverter(string editoralias);
    }
}
