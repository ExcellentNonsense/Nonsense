using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Nonsense {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddSingleton<HtmlEncoder>(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic }));
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
