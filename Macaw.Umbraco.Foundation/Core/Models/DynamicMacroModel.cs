using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using umbraco.cms.businesslogic.macro;
using Umbraco.Core.Models;

namespace Macaw.Umbraco.Foundation.Core.Models
{
    public class DynamicMacroModel : DynamicObject
    {
        public IMacro Macro { get; protected set; }

        public IEnumerable<MacroPropertyModel> Source  { get; protected set; }
        public ISiteRepository Repository { get; protected set; }

        public DynamicMacroModel(IMacro macro, IEnumerable<MacroPropertyModel> propertyValues, ISiteRepository repository)
        {
            Source = propertyValues;
            Macro = macro;
            Repository = repository;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var property = Source.FirstOrDefault(s => s.Key.Equals(binder.Name));
            result = Repository.GetPropertyValue(property);

            return true;
        }

		public virtual bool IsNull()
		{
			return false;
		}
    }
}
