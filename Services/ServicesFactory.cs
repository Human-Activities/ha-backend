using Autofac;
using Microsoft.Extensions.Configuration;
using Services.TokenGenerators;
using Services.TokenValidators;

namespace Services
{
    public class ServicesFactory
    {
        private static IContainer Container { get; }
        static ServicesFactory()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<AccessTokenGenerator>().SingleInstance();
            builder.RegisterType<RefreshTokenGenerator>().SingleInstance();
            builder.RegisterType<RefreshTokenValidator>().SingleInstance();
            builder.RegisterType<AccessTokenValidator>().SingleInstance();
            Container = builder.Build();
        }

        public static AccessTokenGenerator CreateAccessTokenGenerator(IConfiguration configuration)
        {
            return Container.Resolve<AccessTokenGenerator>(new TypedParameter(typeof(IConfiguration), configuration));
        }

        public static RefreshTokenGenerator CreateRefreshTokenGenerator(IConfiguration configuration)
        {
            return Container.Resolve<RefreshTokenGenerator>(new TypedParameter(typeof(IConfiguration), configuration));
        }

        public static RefreshTokenValidator CreateRefreshTokenValidator(IConfiguration configuration)
        {
            return Container.Resolve<RefreshTokenValidator>(new TypedParameter(typeof(IConfiguration), configuration));
        }

        public static AccessTokenValidator CreateAccessTokenValidator(IConfiguration configuration)
        {
            return Container.Resolve<AccessTokenValidator>(new TypedParameter(typeof(IConfiguration), configuration));
        }
    }
}
