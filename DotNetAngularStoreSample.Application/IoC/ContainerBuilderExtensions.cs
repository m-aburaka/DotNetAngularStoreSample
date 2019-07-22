﻿using System.Linq;
using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;

namespace DotNetAngularStoreSample.Application.IoC
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterMediator(this ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>()
                .As<IMediator>();

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        public static void RegisterMediatorRequestHandlers(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<>))
                .AsImplementedInterfaces();

            foreach (var type in assembly.ExportedTypes)
            {
                if (type.GetInterfaces().Any(i => i.Name.Contains("IRequestHandler")))
                {
                    if (type.IsGenericType)
                        builder.RegisterGeneric(type).As(typeof(IRequestHandler<,>));
                    else
                        builder.RegisterType(type).AsImplementedInterfaces();
                }
            }
        }

        public static void RegisterMediatorBehaviours(this ContainerBuilder builder, Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
            {
                if (type.GetInterfaces().Any(i => i.Name.Contains("IPipelineBehavior")))
                {
                    if (type.IsGenericType)
                        builder.RegisterGeneric(type).As(typeof(IPipelineBehavior<,>));
                }
            }
        }

        public static void RegisterServerRepositories(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.Contains("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}