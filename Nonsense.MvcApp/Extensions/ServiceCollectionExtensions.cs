using Microsoft.Extensions.DependencyInjection;
using Nonsense.Common.Utilities;
using Nonsense.MvcApp.Infrastructure;

namespace Nonsense.MvcApp.Extensions {

    public static class ServiceCollectionExtensions {

        public static IMvcBuilder AddFeatureFolders(this IMvcBuilder services) {
            Guard.NotNull(services, nameof(services));

            services.AddMvcOptions(o => o.Conventions.Add(new FeatureControllerModelConvention()))
                .AddRazorOptions(o => {
                    o.AreaViewLocationFormats.Clear();
                    o.ViewLocationFormats.Clear();

                    // {0} - Action Name
                    // {1} - Controller Name
                    // {2} - Area Name

                    o.AreaViewLocationFormats.Add("/Areas/{2}/Features/{feature}/{1}/{0}.cshtml");
                    o.AreaViewLocationFormats.Add("/Areas/{2}/Features/{feature}/{0}.cshtml");
                    o.AreaViewLocationFormats.Add("/Areas/{2}/Features/Shared/{0}.cshtml");
                    o.AreaViewLocationFormats.Add("/Areas/Shared/{0}.cshtml");

                    o.ViewLocationFormats.Add("/Features/{feature}/{1}/{0}.cshtml");
                    o.ViewLocationFormats.Add("/Features/{feature}/{0}.cshtml");
                    o.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

                    o.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
                });

            return services;
        }
    }
}
