using Autofac;
using Nonsense.Application.Gateways.WebServices;
using Nonsense.Infrastructure.WebServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Nonsense.Infrastructure {
    public class InfrastructureModule : Module {
        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            builder
                .RegisterType<FlickrService>()
                .As<IFlickrService>()
                .WithParameter(
                    (p, c) => p.ParameterType == typeof(HttpClient),
                    (p, c) => c.Resolve<IHttpClientFactory>().CreateClient())
                .InstancePerLifetimeScope();
        }
    }
}
