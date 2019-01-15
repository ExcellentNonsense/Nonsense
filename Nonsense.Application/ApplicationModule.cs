using Autofac;
using Nonsense.Application.RandomImages.Interactors;
using Nonsense.Application.Users.Interactors;

namespace Nonsense.Application {

    public class ApplicationModule : Module {

        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            builder
                .RegisterType<GetFlickrImagesInteractor>()
                .As<IGetFlickrImagesInteractor>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CreateUserInteractor>()
                .As<ICreateUserInteractor>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<GetAllUsersInteractor>()
                .As<IGetAllUsersInteractor>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<GetUserByIdInteractor>()
                .As<IGetUserByIdInteractor>()
                .InstancePerLifetimeScope();
        }
    }
}
