using Macaw.Umbraco.Foundation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace Example.Implementation.SurfaceControllers
{
    //Example of how to your own controller for a partial..
    public class PersonSurfaceController : SurfaceController
    {
        protected ISiteRepository Repository;

        public PersonSurfaceController(ISiteRepository rep)
            : base()
        {
            Repository = rep;
        }

        [ChildActionOnly]
        public ActionResult GetPerson(int contentId)
        {
            //call your business services or call your repositories..
            dynamic content = Repository.FindById(contentId);

            //map the documenttype to a typed object..
            return PartialView("Person", new Example.Implementation.ViewModels.Person
            {
                Name = content.Name,
                City = content.Woonplaats
            });
        }
    }
}