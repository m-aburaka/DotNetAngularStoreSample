using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Module = Autofac.Module;

namespace DotNetAngularStoreSample.Application.IoC
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMediator();

            builder.RegisterMediatorRequestHandlers(ThisAssembly);
            builder.RegisterMediatorBehaviours(ThisAssembly);
        }
    }
}
