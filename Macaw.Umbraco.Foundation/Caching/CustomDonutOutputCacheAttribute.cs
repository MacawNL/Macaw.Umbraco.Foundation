using DevTrends.MvcDonutCaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Macaw.Umbraco.Foundation.Caching
{
    /// <summary>
    /// This class inherits from DonutOutputCacheAttribute class.
    /// This attribute is created for getting the method RenderTemplate work
    /// </summary>
    public class CustomDonutOutputCacheAttribute : DonutOutputCacheAttribute
    {
        /// <summary>
        /// Override on result executing method
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!umbraco.library.IsLoggedOn()) // A request has been done from the backoffice, probably umbraco.library.RenderTemplate, do nothing with cache for this request
            {
                base.OnResultExecuting(filterContext);
            }
        }

        /// <summary>
        /// Overrride on result executed method
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!umbraco.library.IsLoggedOn())// A request has been done from the backoffice, probably umbraco.library.RenderTemplate, do nothing with cache for this request
            {
                base.OnResultExecuted(filterContext);
            }
        }

        /// <summary>
        /// Override on action executed method
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!umbraco.library.IsLoggedOn())// A request has been done from the backoffice, probably umbraco.library.RenderTemplate, do nothing with cache for this request
            {
                base.OnActionExecuted(filterContext);
            }
        }

        /// <summary>
        /// Override on actione executing method
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!umbraco.library.IsLoggedOn())// A request has been done from the backoffice, probably umbraco.library.RenderTemplate, do nothing with cache for this request
            {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}