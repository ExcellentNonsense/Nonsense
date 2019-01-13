using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Nonsense.MvcApp.Infrastructure {
    [HtmlTargetElement("a", Attributes = "th-is-active-route")]
    public class ActiveRouteTagHelper : TagHelper {
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContextData { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            string currentController = ViewContextData.RouteData.Values["Controller"].ToString();
            string currentAction = ViewContextData.RouteData.Values["Action"].ToString();

            if (!string.IsNullOrWhiteSpace(Controller) &&
                !string.IsNullOrWhiteSpace(Action)) {

                if (Controller.Equals(currentController, StringComparison.CurrentCultureIgnoreCase)) {

                    if (Action.Equals(currentAction, StringComparison.CurrentCultureIgnoreCase)) {

                        if (output.Attributes.ContainsName("th-nav-header")) {
                            output.Attributes.SetAttribute("class", "th-nav-header__link--active");
                        }
                        else if (output.Attributes.ContainsName("th-nav-secondary")) {
                            output.Attributes.SetAttribute("class", "th-nav-secondary__link--active");
                        }
                    }
                    else if (Controller.Equals("Band", StringComparison.CurrentCultureIgnoreCase) &&
                        output.Attributes.ContainsName("th-nav-header")) {
                        output.Attributes.SetAttribute("class", "th-nav-header__link--active");
                    }
                }
            }

            output.Attributes.RemoveAll("th-nav-header");
            output.Attributes.RemoveAll("th-nav-secondary");
            output.Attributes.RemoveAll("th-is-active-route");
        }
    }
}
