using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nonsense.Infrastructure.WebServices;

namespace Nonsense {
    public class Startup {
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();

            services.AddSingleton<HtmlEncoder>(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic }));

            services.AddHttpClient<IFlickrService, FlickrService>();
        }

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
