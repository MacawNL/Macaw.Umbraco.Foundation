using Macaw.Umbraco.Foundation.Core;
using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using umbraco.dialogs;

namespace Example.Implementation.ViewModels
{
    //public class ContentViewModel : DynamicModel
    //{
    //    public ContentViewModel(IPublishedContent source, ISiteRepository repository)
    //        : base(source, repository)
    //    {

    //    }

    //    /// <summary>
    //    /// As used on the homepage highligted news items list.
    //    /// </summary>
    //    public virtual string MiniIntro
    //    {
    //        get
    //        {
    //            string intro = Dynamic.Intro;
    //            if (intro.Length < 110)
    //                return intro;
    //            else
    //                return string.Format("{0}..", intro.Substring(0, 110));
    //        }
    //    }

    //}

    public class ContentViewModel
    {
        public string BrowserTitel { get; set; }
        public string MetaDescription { get; set; }
        
        public dynamic Homepage { get; set; }
        public dynamic Afbeelding { get; set; }

        public DateTime PublishDate { get; set; }
        public string Titel { get; set; }
        public string Intro { get; set; }
        public HtmlString MainBody { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// As used on the homepage highligted news items list.
        /// </summary>
        public virtual string MiniIntro
        {
            get
            {
                string intro = this.Intro;
                if (intro.Length < 110)
                    return intro;
                else
                    return string.Format("{0}..", intro.Substring(0, 110));
            }
        }

    }
}