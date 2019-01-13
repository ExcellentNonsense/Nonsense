using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;

namespace Nonsense.MvcApp.Infrastructure {
    public class FeatureViewLocationExpander : IViewLocationExpander {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations) {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (viewLocations == null) throw new ArgumentNullException(nameof(viewLocations));

            var controllerDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            var featureName = controllerDescriptor?.Properties["feature"] as string;

            foreach (var location in viewLocations) {
                yield return location.Replace("{feature}", featureName);
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context) {
            context.Values["action_displayname"] = context.ActionContext.ActionDescriptor.DisplayName;
        }
    }
}
