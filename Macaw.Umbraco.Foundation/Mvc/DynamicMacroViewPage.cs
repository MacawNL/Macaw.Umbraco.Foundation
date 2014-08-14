using System;
using Umbraco.Core;
using Umbraco.Web.Macros;
using Umbraco.Core.Models;
using Macaw.Umbraco.Foundation.Core.Models;
using Macaw.Umbraco.Foundation.Core;
using System.Web.Mvc;

namespace Macaw.Umbraco.Foundation.Mvc
{
    public class DynamicMacroViewPage : PartialViewMacroPage
	{
		public ISiteRepository Repository {get; private set;}

        public DynamicMacroViewPage()
            : base()
        {
            Repository = DependencyResolver.Current.GetService<ISiteRepository>();
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
					_macro = Repository.FindMacroByAlias(Model.MacroAlias, (int)CurrentPage.Id, Model.MacroParameters);
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
