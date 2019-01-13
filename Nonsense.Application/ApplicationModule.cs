using Autofac;
using Nonsense.Application.RandomImages.Interactors;

namespace Nonsense.Application {

    public class ApplicationModule : Module {

        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            builder
                .RegisterType<GetFlickrImagesInteractor>()
                .As<IGetFlickrImagesInteractor>()
                .InstancePerLifetimeScope();
        }
    }
}
