using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using AutoMapper;
using Module = Autofac.Module;

namespace DotNetAngularStoreSample.Application.IoC
{
    /// <summary>
    /// DI module, which registers core command handlers and services
    /// </summary>
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterServices(ThisAssembly);

            builder.RegisterMediator();

            builder.RegisterMediatorRequestHandlers(ThisAssembly);
            builder.RegisterMediatorBehaviours(ThisAssembly);
            
            builder.RegisterInstance(SerilogFactory.Get())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterInstance(MapperFactory.Get()).As<IMapper>();
        }
    }
}
