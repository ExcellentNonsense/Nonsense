using Autofac;
using Nonsense.MvcApp.Features.Band;

namespace Nonsense.MvcApp {

    public class MvcAppModule : Module {

        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            builder
                .RegisterType<GetFlickrImagesPresenter>()
                .InstancePerLifetimeScope();
        }
    }
}
