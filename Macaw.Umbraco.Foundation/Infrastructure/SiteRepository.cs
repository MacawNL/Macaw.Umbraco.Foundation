using Examine;
using Examine.LuceneEngine.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Models;
using Macaw.Umbraco.Foundation.Core.Models;
using Macaw.Umbraco.Foundation.Core;

namespace Macaw.Umbraco.Foundation.Infrastructure
{
    public class SiteRepository : ISiteRepository
    {
        //protected IMediaService MediaService;
        protected IContentService Service;
        protected UmbracoHelper Helper;
        public string SearchProvidername { get; private set; }

        public SiteRepository(IContentService service, UmbracoHelper helper)
			: this(service, helper, "ExternalSearcher") //use umbraco default searcher.
		{
		}

        public SiteRepository(IContentService service, UmbracoHelper helper, string searchProvidername)
        {
            Service = service;
            SearchProvidername = searchProvidername;
            Helper = helper;
        }

        /// <summary>
        /// Find a dynamic model based on the given query, when mainbody exist it is used for highlighting, otherwise nothing is highlighted.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<DynamicModel> Find(string query)
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection[SearchProvidername];
            var criteria = searcher.CreateSearchCriteria(Examine.SearchCriteria.BooleanOperation.Or);
            var searchCriteria = criteria.RawQuery(query);
            var results = searcher.Search(searchCriteria);

            foreach (var item in results)
            {
                Dictionary<string, string> fields = item.Fields.Where(f => f.Value.ToUpper()
                    .Contains(query.ToUpper())
                    || f.Value.ToUpper().Split(new[] { ' ' })
                        .Any(val => query.Split(new[] { ' ' })
                            .Contains(val))).ToDictionary(x => x.Key, x => x.Value);

                var field = fields.Keys.Contains("mainbody", StringComparer.InvariantCultureIgnoreCase) ? 
                    fields.Where(f => f.Key.ToLower() == "mainbody")
                        .FirstOrDefault() : fields.FirstOrDefault();

                string searchHiglight = !String.IsNullOrEmpty(field.Key) ?
                    LuceneHighlightHelper.Instance.GetHighlight(field.Value, field.Key, ((LuceneSearcher)searcher).GetSearcher(), query) : String.Empty;

                var ret = new DynamicSearchResultItem(Helper.TypedContent(item.Id).AsDynamic(), this);
                ret.HighlightText = searchHiglight;

                yield return ret;
            }
        }

		public IEnumerable<DynamicModel> FindAll()
		{
			List<DynamicModel> result = new List<DynamicModel>();
			var roots = Helper.ContentAtRoot() as IEnumerable<IPublishedContent>;
            return FindAll(roots.Select(n => new DynamicModel(n, this)));
		}


        public IEnumerable<DynamicModel> FindAll(IEnumerable<DynamicModel> rootNodes)
		{
            var result = new List<DynamicModel>();
            foreach (var node in rootNodes)
            {
                result.Add(node);
                result.AddRange(node.Children().Select(n => new DynamicModel(n, this)));
            }

            return result;
		}

		public DynamicModel FindById(int id)
		{
			return new DynamicModel(Helper.TypedContent(id), this);
		}

		public IContent FindContentById(int id)
		{
			return Service.GetById(id);
		}

        public DynamicModel FindMediaById(int id)
        {
			return new DynamicModel(Helper.TypedMedia(id), this);
        }

		public string Translate(string key)
		{
			return Helper.GetDictionaryValue(key);
		}
		
		public DynamicMacroModel FindMacroById(int id, IDictionary<string, object> values)
		{
			//todo: move this part to the repository aswell.
			var macro = umbraco.cms.businesslogic.macro.Macro.GetById(id);
			var macroModel = new umbraco.cms.businesslogic.macro.MacroModel(macro);

			var source = values.Join(macroModel.Properties,
				mod => mod.Key, mm => mm.Key,
				(mod, mm) => new umbraco.cms.businesslogic.macro.MacroPropertyModel(mod.Key, mod.Value.ToString(), mm.Type, mm.CLRType));

			return new DynamicMacroModel(source, this);
		}

		public string NiceUrl(IPublishedContent content)
		{
			return NiceUrl(content.Id);
		}

		public string NiceUrl(int id)
		{
			return Helper.NiceUrlWithDomain(id);
		}
	}
}