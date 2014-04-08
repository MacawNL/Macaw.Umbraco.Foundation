using Macaw.Umbraco.Foundation.Core.Models;
using System;
using Umbraco.Core;
using Umbraco.Core.Models;
using System.Web.Mvc;
using System.Reflection; //for service locator.
using Umbraco.Web;
using System.Collections.Generic;

namespace Macaw.Umbraco.Foundation.Core
{
    public static class Mapper
    {
		/// <summary>
		/// Turn your IPublishedContent model into your own typed version.
		/// Supported are models with a parameterless constructor and DynamicModel ViewModels.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content"></param>
		/// <returns></returns>
		public static T As<T>(this IPublishedContent content)
		{
		    if (content is T) //besure you don't convert to the same type!
		        throw new ArgumentException(String.Format("You try to convert a type <{0}> into it's own typed", typeof(T).FullName));

            
            if (typeof (T).Inherits<DynamicModel>())
		    {
		        ISiteRepository repo;

		        var dyn = content as DynamicModel;
		        if (dyn != null) //keep using already defined repository.
		            repo = dyn.Repository;
		        else //get repository from service locator.
                    repo = DependencyResolver.Current.GetService<ISiteRepository>();

		        return (T) Activator.CreateInstance(typeof (T), content, repo);
		    }
		    else
            {
                #region General mapping construction

                //if you don't use the Macaw.Umbraco.Foundation, you only need this region, to create a simple IPublishContent -> ViewModel mapper.

                var obj = Activator.CreateInstance<T>();

                foreach (var prop in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty))
		        {
		            if (content.HasProperty(prop.Name))
		            {
		                prop.SetValue(obj, content.GetPropertyValue(prop.Name), null);
		            }
                    else if (content.HasProperty(char.ToLower(prop.Name[0]) + prop.Name.Substring(1))) //lowercase starting character for alias
                    {
                        prop.SetValue(obj, content.GetPropertyValue(char.ToLower(prop.Name[0]) + prop.Name.Substring(1)), null);
                    }
                    else
                    {
                        var sourceProp = content.GetType().GetProperty(prop.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                        if (sourceProp != null)
                        {
                            prop.SetValue(obj, sourceProp.GetValue(content), null);
                        }
                    }
		        }

		        return obj;

                #endregion
            }
		}

        public static IEnumerable<T> As<T>(this IEnumerable<IPublishedContent> items)
        {
            return items.ForEach(i => i.As<T>());
        }
    }
}