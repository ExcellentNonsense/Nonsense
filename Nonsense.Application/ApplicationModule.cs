﻿using Autofac;
using Nonsense.Application.RandomImages;
using Nonsense.Application.Users;

namespace Nonsense.Application {

    public class ApplicationModule : Module {

        protected override void Load(ContainerBuilder builder) {
            base.Load(builder);

            builder
                .RegisterType<AccountService>()
                .As<IAccountService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RandomImagesService>()
                .As<IRandomImagesService>()
                .InstancePerLifetimeScope();
        }
    }
}
