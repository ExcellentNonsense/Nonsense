using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Linq;

namespace Nonsense.MvcApp.Infrastructure {

    class FeatureControllerModelConvention : IControllerModelConvention {

        public void Apply(ControllerModel model) {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var featureName = GetFeatureName(model);
            model.Properties.Add("feature", featureName);
        }

        private string GetFeatureName(ControllerModel model) {
            var result = model.ControllerType.FullName
                .Split('.')
                .SkipWhile(s => s != "Features")
                .Skip(1)
                .Take(1)
                .FirstOrDefault();

            return result;
        }
    }
}
