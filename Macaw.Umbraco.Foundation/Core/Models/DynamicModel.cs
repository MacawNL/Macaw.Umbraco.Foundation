using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Macaw.Umbraco.Foundation.Core.Models
{

	//todo: add translation to model..

	/// <summary>
	/// A DynamicModel is a smart model..
	/// </summary>
    public class DynamicModel : DynamicPublishedContent, INullModel
    {
        protected IPublishedContent Source;
		protected ISiteRepository Repository;

		public DynamicModel(IPublishedContent source, ISiteRepository repository)
            :base(source)
        {
            Source = source;
			Repository = repository;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var ret = base.TryGetMember(binder, out result);
            return ret;
        }

        public dynamic Homepage
        {
            get
            {
                return new DynamicModel(Source.AncestorOrSelf(), Repository);
            }
        }

		public DateTime PublishDate //late binding of the publishdate...
		{
			get
			{
				var content = Repository.FindContentById(this.Id);
				var ret = content.ReleaseDate;

				if (ret == null)
				{
					return this.UpdateDate;
				}
				else
				{
					return ret.Value;
				}
			}
		}

		public bool IsNull()
		{
			return false;
		}
	}
}
