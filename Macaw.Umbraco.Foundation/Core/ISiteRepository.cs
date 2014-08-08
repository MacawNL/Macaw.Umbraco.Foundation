using Macaw.Umbraco.Foundation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umbraco.Core.Models;

namespace Macaw.Umbraco.Foundation.Core
{
    public interface ISiteRepository
    {
		string Translate(string key);

        /// <summary>
        /// Get home page
        /// </summary>
        /// <returns></returns>
        DynamicModel GetHomePage();

		/// <summary>
		/// Find the macro en return it with content.
		/// </summary>
		/// <param name="id">Id of the macro</param>
		/// <param name="values">published values of the macro..</param>
		/// <returns></returns>
		DynamicMacroModel FindMacroById(int id, IDictionary<string, object> values);
		DynamicMediaModel FindMediaById(int id);
        DynamicModel FindById(int id);
		IContent FindContentById(int id);

        IEnumerable<DynamicModel> Find(string query);
		IEnumerable<DynamicModel> FindAll();

		/// <summary>
		/// Returns a friendly url with domainname
		/// </summary>
		/// <param name="id">Content / Node Id</param>
		/// <returns></returns>
		string FriendlyUrl(int id);
		string FriendlyUrl(IPublishedContent content);
    }
}
