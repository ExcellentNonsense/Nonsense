using Autofac;
using Nonsense.Application.Gateways.Repositories;
using Nonsense.Application.Gateways.WebServices;
using Nonsense.Infrastructure.Data;
using Nonsense.Infrastructure.WebServices;
using System.Net.Http;

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

            builder
                .RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
