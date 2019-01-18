using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nonsense.MvcApp.Infrastructure.Filters {

    public class ValidateModelStateAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context) {
            base.OnActionExecuting(context);

            if (!context.ModelState.IsValid) {
                var controller = context.Controller as Controller;

                if (controller != null &&
                    context.ActionArguments.TryGetValue("model", out object model)) {

                    context.Result = controller.View(model);
                }
                else {
                    context.Result = new BadRequestResult();
                }
            }
        }
    }
}
