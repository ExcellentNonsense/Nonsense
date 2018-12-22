using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Nonsense.Infrastructure.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Nonsense.Tests {
    public class ActiveRouteTagHelperTests {
        [Fact]
        public void ProcessTag_ActiveLink_AddClass() {
            // Arrange
            var context = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "myuniqueid");

            var output = new TagHelperOutput("a",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult<TagHelperContent> (new DefaultTagHelperContent()));

            var viewContext = new ViewContext();
            viewContext.RouteData = new RouteData();
            viewContext.RouteData.Values.Add("Controller", "Home");
            viewContext.RouteData.Values.Add("Action", "Index");

            // Act
            var tagHelper = new ActiveRouteTagHelper {
                Controller = "Home",
                Action = "Index",
                ViewContextData = viewContext
            };
            tagHelper.Process(context, output);

            // Assert
            Assert.Equal("nav-header__active-link", output.Attributes["class"].Value);
        }

        [Fact]
        public void ProcessTag_InactiveLink_NotAddClass() {
            // Arrange
            var context = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "myuniqueid");

            var output = new TagHelperOutput("a",
                new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

            var viewContext = new ViewContext();
            viewContext.RouteData = new RouteData();
            viewContext.RouteData.Values.Add("Controller", "Home");
            viewContext.RouteData.Values.Add("Action", "Index");

            // Act
            var tagHelper = new ActiveRouteTagHelper {
                Controller = "Profile",
                Action = "Index",
                ViewContextData = viewContext
            };
            tagHelper.Process(context, output);

            // Assert
            Assert.Null(output.Attributes["class"]);
        }
    }
}
