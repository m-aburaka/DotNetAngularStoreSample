using Autofac;
using DotNetAngularStoreSample.Application.IoC;
using DotNetAngularStoreSample.Repository.Ef;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace DotNetAngularStoreSample.Server.IoC
{
    public class ServerModule : Module
    {
        private readonly IConfiguration _configuration;

        public ServerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterModule<ApplicationModule>();
            builder.RegisterServerRepositories(typeof(EfRepositoryAssemblyReference).Assembly);

            builder
                .RegisterType<AppDbContext>()
                .WithParameter("options", DbContextOptionsFactory.Get(_configuration))
                .SingleInstance();
        }
    }
}
