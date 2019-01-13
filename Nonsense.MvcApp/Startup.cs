using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Nonsense.Application;
using Nonsense.Infrastructure;
using Nonsense.MvcApp.Extensions;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Nonsense.MvcApp {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().AddFeatureFolders();

            // Create the HttpClientFactory to provide the HttpClient to the Typed Client classes.
            services.AddHttpClient();

            // Display Cyrillic characters in HTML without encoding.
            services.AddSingleton(HtmlEncoder
                .Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic }));
        }

        // Register services with Autofac container.
        public void ConfigureContainer(ContainerBuilder builder) {
            builder.RegisterModule(new MvcAppModule());
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new InfrastructureModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Dev", action = "SiteMap" }
                );

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}",
                    defaults: new { action = "Index" });
            });
        }
    }
}
