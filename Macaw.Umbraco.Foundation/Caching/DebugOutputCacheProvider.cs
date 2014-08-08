using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Caching;
using Umbraco.Core.Logging;

namespace Macaw.Umbraco.Foundation.Caching
{
	/// <summary>
	/// Is a dummy cache provider which for debuging distributed caching 
	/// </summary>
	public class DebugOutputCacheProvider : OutputCacheProvider, IEnumerable<KeyValuePair<string, object>>
	{
		protected void LogMessage(string message)
		{
			Debug.WriteLine(message);
			Console.WriteLine(message);
			LogHelper.Info(typeof(DebugOutputCacheProvider), message);
		}

		public override object Get(string key)
		{
			string message = string.Format("Get '{0}' from cache", key);
			LogMessage(message);
			return null;
		}

		public override object Add(string key, object entry, DateTime utcExpiry)
		{
			string message = string.Format("Add '{0}' of type '{1}' which expires at: {2} ", key, entry.GetType().FullName, utcExpiry);
			LogMessage(message);
			return null;
		}

		public override void Set(string key, object entry, DateTime utcExpiry)
		{
			string message = string.Format("Set '{0}' of type '{1}' which expires at: {2} ", key, entry.GetType().FullName, utcExpiry);
			LogMessage(message);
		}

		public override void Remove(string key)
		{
			string message = string.Format("Remove '{0}' from cache", key);
			LogMessage(message);
		}

		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			string message = string.Format("GetEnumerator from cache");
			return (new Dictionary<string, object>()).GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			string message = string.Format("GetEnumerator from cache");
			return (new Dictionary<string, object>()).GetEnumerator();
		}
	}
}
