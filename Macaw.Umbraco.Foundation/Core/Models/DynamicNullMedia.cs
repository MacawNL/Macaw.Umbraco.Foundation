using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Umbraco.Core.Dynamics;

namespace Macaw.Umbraco.Foundation.Core.Models
{
	public class DynamicNullMedia : DynamicObject, INullModel, IEnumerable, IHtmlString
	{
		//Same usage as UmbracoCore DynamicNull
		public static readonly DynamicNullMedia Null = new DynamicNullMedia(DynamicNull.Null);

		private DynamicNull _dynamicNull;

		private DynamicNullMedia(DynamicNull dn)
		{
			_dynamicNull = dn;
		}

		public string Url
		{
			get
			{
				var ret = WebConfigurationManager.AppSettings["Macaw.Umbraco.Foundation.EmptyImageUrl"];
				return ret == null ? string.Empty : ret;
			}
		}

		public bool IsNull()
		{
			return true;
		}

		public IEnumerator GetEnumerator()
		{
			return _dynamicNull.GetEnumerator();
		}

		public string ToHtmlString()
		{
			return _dynamicNull.ToHtmlString();
		}

		//DynamicNull proxy functions
		public DynamicNull ToContentSet()
		{
			return _dynamicNull.ToContentSet();
		}

		public int Count()
		{
			return _dynamicNull.Count();
		}

		public bool HasValue()
		{
			return _dynamicNull.HasValue();
		}

		//Dynamic Object

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			return _dynamicNull.TryGetMember(binder, out result);
		}

		public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			return _dynamicNull.TryGetIndex(binder, indexes, out result);
		}

		public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
		{
			return _dynamicNull.TryInvoke(binder, args, out result);
		}

		//base functions

		public override string ToString()
		{
			return _dynamicNull.ToString();
		}
	}
}
