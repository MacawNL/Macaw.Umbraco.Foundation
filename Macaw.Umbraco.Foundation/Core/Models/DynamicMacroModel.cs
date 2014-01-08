using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umbraco.cms.businesslogic.macro;
using Umbraco.Core.Dynamics;
using Umbraco.Core;

namespace Macaw.Umbraco.Foundation.Core.Models
{
	public class DynamicMacroModel : DynamicObject, INullModel
    {
        protected IEnumerable<MacroPropertyModel> Source;
        protected ISiteRepository Repository;

        public DynamicMacroModel(IEnumerable<MacroPropertyModel> source, ISiteRepository repository)
        {
            Source = source;
            Repository = repository;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var property = Source.FirstOrDefault(s => s.Key.Equals(binder.Name));

			if (property != null)
			{
				switch (property.Type)
				{
					case Constants.PropertyEditors.ContentPickerAlias:
						result = Repository.FindById(int.Parse(property.Value));
						break;
					case Constants.PropertyEditors.MediaPickerAlias:
						result = Repository.FindMediaById(int.Parse(property.Value));
						break;
					default:
						result = property.Value;
						break;
				}

				if (result == null) //convert null to dynamicnull
					result = DynamicNull.Null;
			}
			else
				result = DynamicNull.Null;

            return true;
        }

		public virtual bool IsNull()
		{
			return false;
		}
	}
}
