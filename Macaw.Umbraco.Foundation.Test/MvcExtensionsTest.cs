using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Macaw.Umbraco.Foundation.Mvc;
using Website.Test;
using Umbraco.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Macaw.Umbraco.Foundation.Test
{
    [TestClass]
    public class MvcExtensionsTest // Naming convention: Method_to_test__State_under_test__Expected_behavior
    {
		/// <summary>
		/// Single item test
		/// </summary>
		[TestMethod]
		public void ToJson__Include_hidden_items_set_to_false__Json_output_returns_nonhidden_item()
		{
			//1. Arrange
			System.Web.Mvc.HtmlHelper helper = null;
			var mContent = Arrange.Content("Consectetur hidden item", true);

			//2. act
			var output = helper.ToJson(mContent.Object, null, false);

			//3. assert
			Assert.IsTrue(output.ToString().Equals(string.Empty), "Ouput does contain hidden item");
		}

		/// <summary>
		/// Single item test
		/// </summary>
		[TestMethod]
		public void ToJson__Include_hidden_items_not_set__Json_output_returns_nonhidden_item()
		{
			//1. Arrange
			System.Web.Mvc.HtmlHelper helper = null;
			var mContent = Arrange.Content("Consectetur hidden item", true);

			//2. act
			var output = helper.ToJson(mContent.Object, null);

			//3. assert
			Assert.IsTrue(output.ToString().Contains("Consectetur hidden item"), "Ouput does not contains item");
		}

        /// <summary>
        /// IEnumerable testWhen IncludeHiddenItems is set to false only non hidden items have to be rendered as json
        /// </summary>
        [TestMethod]
        public void ToJson__Include_hidden_items_set_to_false__Json_output_returns_nonhidden_items()
        {
            //1. Arrange
            System.Web.Mvc.HtmlHelper helper = null;
            var mContent = Arrange.Content("lorem parent page", //setup a page with 5 child objects
                new List<IPublishedContent>()
				{
					{ Arrange.Content("Lorem child page 1").Object },
					{ Arrange.Content("Ipsum child page 2").Object },
					{ Arrange.Content("Ipsum child page 3").Object },
					{ Arrange.Content("Sit child page 4").Object },
					{ Arrange.Content("Consectetur child page 5", true).Object }
				});

            //2. act
            var output = helper.ToJson(mContent.Object.Children, null, false);

            //3. assert
            Assert.IsTrue(output.ToString().Contains("Lorem child page 1"), "Ouput does not contains non-hidden child 1");
            Assert.IsTrue(output.ToString().Contains("Ipsum child page 2"), "Ouput does not contains non-hidden child 2");
            Assert.IsTrue(output.ToString().Contains("Ipsum child page 3"), "Ouput does not contains non-hidden child 3");
            Assert.IsTrue(output.ToString().Contains("Sit child page 4"), "Ouput does not contains non-hidden child 4");
            Assert.IsTrue(!output.ToString().Contains("Consectetur child page 5"), "Ouput does contains hidden child 5");

        }

        /// <summary>
        /// Include hidden items aswell.
        /// </summary>
        [TestMethod]
        public void ToJson__Include_hidden_items_not_set__Json_output_returns_all_items()
        {
            //1. Arrange
            System.Web.Mvc.HtmlHelper helper = null;
            var mContent = Arrange.Content("lorem parent page", //setup a page with 5 child objects
                new List<IPublishedContent>()
				{
					{ Arrange.Content("Lorem child page 1").Object },
					{ Arrange.Content("Ipsum child page 2").Object },
					{ Arrange.Content("Ipsum child page 3").Object },
					{ Arrange.Content("Sit child page 4").Object },
					{ Arrange.Content("Consectetur child page 5", true).Object }
				});

            //2. act
            var output = helper.ToJson(mContent.Object.Children, null);

            //3. assert
            Assert.IsTrue(output.ToString().Contains("Lorem child page 1"), "Ouput does not contains non-hidden child 1");
            Assert.IsTrue(output.ToString().Contains("Ipsum child page 2"), "Ouput does not contains non-hidden child 2");
            Assert.IsTrue(output.ToString().Contains("Ipsum child page 3"), "Ouput does not contains non-hidden child 3");
            Assert.IsTrue(output.ToString().Contains("Sit child page 4"), "Ouput does not contains non-hidden child 4");
            Assert.IsTrue(output.ToString().Contains("Consectetur child page 5"), "Ouput does not contains hidden child 5");

        }

        /// <summary>
        /// For IPublishedContent you can specify which properties are part of the json output.
        /// </summary>
        [TestMethod]
        public void ToJson__Some_aliasses_specified__Json_output_only_contains_specified_aliasses()
        {
            //1. Arrange
            System.Web.Mvc.HtmlHelper helper = null;
            var mContent = Arrange.Content("lorem parent page", //setup a page with 5 child objects
                new List<IPublishedContent>()
				{
					{ Arrange.Content("Lorem child page 1").Object },
					{ Arrange.Content("Ipsum child page 2").Object },
					{ Arrange.Content("Ipsum child page 3").Object },
					{ Arrange.Content("Sit child page 4").Object },
					{ Arrange.Content("Consectetur child page 5", true).Object }
				});

            //2. act
            var output = helper.ToJson(mContent.Object.Children, new[] {"title"});

            //3. assert
            Assert.IsTrue(!output.ToString().Contains("mainBody"), "Output contains mainbody, but only title has to be returned");
            Assert.IsTrue(output.ToString().Contains("title"), "Output does not contain title");
        }

        /// <summary>
        /// Test Json output for a custom object, this can be an anonymous object aswell as your own typed object.
        /// actualy this is testing the newtonsoft json implementation which is hopefully correct.
        /// </summary>
        [TestMethod]
        public void ToJson__Custom_object__A_normal_json_convert_is_executed() //actualy this is testing the newtonsoft json implementation which is hopefully correct.
        {
            //1. Arrange
            System.Web.Mvc.HtmlHelper helper = null;
            var obj = new {
                Name = "John Doe",
                City = "New York",
                Age = 38
            };

            //2. Act
            System.Web.Mvc.MvcHtmlString output = helper.ToJson(obj);

            //3. Assert
            Assert.IsTrue(output.ToString().Equals("{\"Name\":\"John Doe\",\"City\":\"New York\",\"Age\":38}"));
        }
    }
}
