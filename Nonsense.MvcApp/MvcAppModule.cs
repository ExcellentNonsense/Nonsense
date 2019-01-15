using Autofac;
using Nonsense.MvcApp.Areas.Admin.Features.Users;
using Nonsense.MvcApp.Features.Band;

namespace Nonsense.MvcApp {

    public class MvcAppModule : Module {

        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            builder
                .RegisterType<GetFlickrImagesPresenter>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<CreateUserPresenter>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<GetAllUsersPresenter>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<GetUserByIdPresenter>()
                .InstancePerLifetimeScope();
        }
    }
}
