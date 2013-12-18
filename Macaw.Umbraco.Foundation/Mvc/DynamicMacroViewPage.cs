using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.Macros;
using Umbraco.Core.Models;
using Macaw.Umbraco.Foundation.Core.Models;
using Macaw.Umbraco.Foundation.Core;

namespace Macaw.Umbraco.Foundation.Mvc
{
    public class DynamicMacroViewPage : PartialViewMacroPage
	{
		public ISiteRepository Repository {get; set;}

        public DynamicMacroViewPage()
            : base()
        {
            
        }

		public new dynamic CurrentPage
		{
			get
			{
				var repo = ServiceLocator.Current.GetInstance<ISiteRepository>();
				return new DynamicModel((base.CurrentPage as IPublishedContent), repo);
			}
		}

		/// <summary>
		/// Access to all Macro parameters by using this dynamic property.
		/// Returns a "DynamicMacroModel"
		/// </summary>
        public virtual dynamic Macro
        {
            get
            {
				var repo = ServiceLocator.Current.GetInstance<ISiteRepository>();
				return repo.FindMacroById(Model.MacroId, Model.MacroParameters);
            }
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
	}
}
