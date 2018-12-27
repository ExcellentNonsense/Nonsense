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
        public void ProcessTag_ActiveHeaderLink_AddClass() {
            // Arrange
            var env = new TagHelperEnvironment("Home", "Index");

            env.Output.Attributes.Add("th-nav-header", null);

            // Act
            var tagHelper = new ActiveRouteTagHelper {
                Controller = "Home",
                Action = "Index",
                ViewContextData = env.ViewContext
            };
            tagHelper.Process(env.Context, env.Output);

            // Assert
            Assert.Equal("th-nav-header__link--active", env.Output.Attributes["class"].Value);
        }

        [Fact]
        public void ProcessTag_ActiveHeaderBandLink_AddClass() {
            // Arrange
            var env = new TagHelperEnvironment("Band", "Index");

            env.Output.Attributes.Add("th-nav-header", null);

            // Act
            var tagHelper = new ActiveRouteTagHelper {
                Controller = "Band",
                Action = "MyBand",
                ViewContextData = env.ViewContext
            };
            tagHelper.Process(env.Context, env.Output);

            // Assert
            Assert.Equal("th-nav-header__link--active", env.Output.Attributes["class"].Value);
        }

        [Fact]
        public void ProcessTag_ActiveSecondaryLink_AddClass() {
            // Arrange
            var env = new TagHelperEnvironment("Band", "FlickrBand");

            env.Output.Attributes.Add("th-nav-secondary", null);

            // Act
            var tagHelper = new ActiveRouteTagHelper {
                Controller = "Band",
                Action = "FlickrBand",
                ViewContextData = env.ViewContext
            };
            tagHelper.Process(env.Context, env.Output);

            // Assert
            Assert.Equal("th-nav-secondary__link--active", env.Output.Attributes["class"].Value);
        }

        [Fact]
        public void ProcessTag_InactiveHeaderLink_NotAddClass() {
            // Arrange
            var env = new TagHelperEnvironment("Home", "Index");

            env.Output.Attributes.Add("th-nav-header", null);

            // Act
            var tagHelper = new ActiveRouteTagHelper {
                Controller = "Profile",
                Action = "Index",
                ViewContextData = env.ViewContext
            };
            tagHelper.Process(env.Context, env.Output);

            // Assert
            Assert.Null(env.Output.Attributes["class"]);
        }

        private class TagHelperEnvironment {
            public TagHelperContext Context { get; set; }
            public TagHelperOutput Output { get; set; }
            public ViewContext ViewContext { get; set; }

            public TagHelperEnvironment(string requestedController, string requestedAction) {
                Context = new TagHelperContext(
                    new TagHelperAttributeList(),
                    new Dictionary<object, object>(),
                    "myuniqueid");

                Output = new TagHelperOutput("a",
                    new TagHelperAttributeList(),
                    (cache, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

                ViewContext = new ViewContext();
                ViewContext.RouteData = new RouteData();
                ViewContext.RouteData.Values.Add("Controller", requestedController);
                ViewContext.RouteData.Values.Add("Action", requestedAction);
            }
        }
    }
}
