﻿using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nonsense.Application;
using Nonsense.Infrastructure;
using Nonsense.Infrastructure.Identity;
using Nonsense.MvcApp.Extensions;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Nonsense.MvcApp {

    public class Startup {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().AddFeatureFolders();

            // Create the HttpClientFactory to provide the HttpClient to the Typed Client classes.
            services.AddHttpClient();

            // Display Cyrillic characters in HTML without encoding.
            services.AddSingleton(HtmlEncoder
                .Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic }));

            services.AddDbContext<AppIdentityDbContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(o => o.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }

        // Register services with Autofac container.
        public void ConfigureContainer(ContainerBuilder builder) {
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new InfrastructureModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: null,
                    template: "{area:exists}/{controller=Users}/{action=Index}"
                );

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action=Index}"
                );

                // For initial development.
                routes.MapRoute(
                    name: null,
                    template: "{controller=Dev}/{action=SiteMap}"
                );
            });
        }
    }
}
