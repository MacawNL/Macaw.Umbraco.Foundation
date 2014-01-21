using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.Macros;
using Umbraco.Core.Models;
using Macaw.Umbraco.Foundation.Core.Models;
using Macaw.Umbraco.Foundation.Core;
using System.Web.Mvc;

namespace Macaw.Umbraco.Foundation.Mvc
{
    public class DynamicMacroViewPage : PartialViewMacroPage
	{
		public ISiteRepository Repository {get; set;}

        public DynamicMacroViewPage()
            : base()
        {
            
        }

		private dynamic _currentPage;
		public new dynamic CurrentPage
		{
			get
			{
				if (_currentPage == null)
				{
					_currentPage = (base.CurrentPage as IPublishedContent).As<DynamicModel>();
				}

				return _currentPage;
			}
		}

		private dynamic _macro;
		/// <summary>
		/// Access to all Macro parameters by using this dynamic property.
		/// Returns a "DynamicMacroModel"
		/// </summary>
        public virtual dynamic Macro
        {
            get
            {
				if (_macro == null)
				{
					var repo = DependencyResolver.Current.GetService<ISiteRepository>();
					_macro = repo.FindMacroById(Model.MacroId, Model.MacroParameters);
				}

				return _macro;
            }
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
	}
}
