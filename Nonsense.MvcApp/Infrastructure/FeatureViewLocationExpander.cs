using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using Nonsense.Common.Utilities;
using System.Collections.Generic;

namespace Nonsense.MvcApp.Infrastructure {

    public class FeatureViewLocationExpander : IViewLocationExpander {

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations) {
            Guard.NotNull(context, nameof(context));
            Guard.NotNull(viewLocations, nameof(viewLocations));

            var controllerDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            var featureName = controllerDescriptor?.Properties["feature"] as string;

            foreach (var location in viewLocations) {
                yield return location.Replace("{feature}", featureName);
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context) {
            Guard.NotNull(context, nameof(context));

            context.Values["action_displayname"] = context.ActionContext.ActionDescriptor.DisplayName;
        }
    }
}
