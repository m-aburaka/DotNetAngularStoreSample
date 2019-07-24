using Autofac;
using DotNetAngularStoreSample.Application.IoC;
using DotNetAngularStoreSample.Repository.Ef;

namespace DotNetAngularStoreSample.Server.Tests.IoC
{
    /// <summary>
    /// DI module, which registers the core application module with mock database
    /// </summary>
    public class ApplicationTestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterServerRepositories(typeof(EfRepositoryAssemblyReference).Assembly);
            
            builder
                .RegisterType<AppDbContext>()
                .WithParameter("options", TestDbContextOptionsFactory.Get())
                .InstancePerLifetimeScope();
        }
    }
}
