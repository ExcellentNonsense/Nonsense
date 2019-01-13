using Autofac;
using Nonsense.MvcApp.Features.Band;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
