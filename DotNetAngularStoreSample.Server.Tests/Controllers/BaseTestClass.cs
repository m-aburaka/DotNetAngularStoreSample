using System;
using Autofac;
using AutoFixture;
using DotNetAngularStoreSample.Repository.Ef;
using DotNetAngularStoreSample.Server.Tests.IoC;
using MediatR;

namespace DotNetAngularStoreSample.Server.Tests.Controllers
{
    /// <summary>
    /// Base class for all controllers tests.
    /// Builds DI container with mock infrastructure, and creates life time scope to be used in tests
    /// </summary>
    public class BaseTestClass
    {
        private readonly IContainer _container;

        public Random Random { get; } = new Random();
        public Fixture AutoFixture { get; } = new Fixture();
        
        public ILifetimeScope Scope { get; set; }
        public IMediator Mediator => Scope.Resolve<IMediator>();


        public BaseTestClass()
        {
            _container = BuildContainer();
            EnsureDbCreated();
            Scope = _container.BeginLifetimeScope();
        }
        
        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<ApplicationTestModule>();
            var container = builder.Build();
            return container;
        }

        private void EnsureDbCreated()
        {
            using (var tmpScope = _container.BeginLifetimeScope())
            using (var context = tmpScope.Resolve<AppDbContext>())
                context.Database.EnsureCreated();
        }
    }
}
