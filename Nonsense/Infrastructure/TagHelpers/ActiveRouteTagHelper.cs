using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Nonsense.Infrastructure.TagHelpers {
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
                !string.IsNullOrWhiteSpace(Action) &&
                Controller.Equals(currentController, StringComparison.CurrentCultureIgnoreCase) &&
                Action.Equals(currentAction, StringComparison.CurrentCultureIgnoreCase)) {
                output.Attributes.SetAttribute("class", "nav-header__active-link");
            }

            output.Attributes.RemoveAll("th-is-active-route");
        }
    }
}
