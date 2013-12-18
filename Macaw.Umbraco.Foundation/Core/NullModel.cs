using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Macaw.Umbraco.Foundation.Models
{
    /// <summary>
	/// For debugging purpose.
	/// </summary>
    public class NullModel : DynamicObject //, INullModel
    {
        internal NullModel()
        {
        }

        public override bool TryGetMember(System.Dynamic.GetMemberBinder binder, out object result)
        {
            result = new NullModel();
            return true;
        }

        public override string ToString()
        {
            return "-- Property is empty, null or does not exist --";
        }
    }
}
