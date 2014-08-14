using Example.Implementation.ViewModels;
using Macaw.Umbraco.Foundation.Controllers;
using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using Macaw.Umbraco.Foundation.Mvc;
using Umbraco.Core.Dynamics;

namespace Example.Controllers
{
    public class ContentController : DynamicBaseController //DocumentType
    {
        public ContentController(ISiteRepository rep)
            : base(rep)
        {
        }

        public override System.Web.Mvc.ActionResult Index(Umbraco.Web.Models.RenderModel model) //base
        {
            //example 2: build your view model
                //var content = model.Content.As<ContentViewModel>();
                //content.Homepage = DynamicNull.Null; //define your typed homepage type here..
            //or in our case this also works, because the dynamicmodel contains a property homepage which can directly be mapped to the viewmodel aswell.
                var content = model.Content.As<DynamicModel>().As<ContentViewModel>();

            //default: return View(model.Content.As<DynamicModel>()); //you actually don't need a custom controller for this.
            //example 1: return View(model.Content.As<ContentViewModel>()); //hybride viewmodel (proxy)
            //example 2: 
            return View(content); //typed viewmodel
        }

        public System.Web.Mvc.ActionResult NewsOld(Umbraco.Web.Models.RenderModel model) //template name
        {
            //default umbraco
            var template = ControllerContext.RouteData.Values["action"].ToString();
            return View(template, model);
        }
    }
}