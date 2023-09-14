using APIPix.Business.ChavePixBusiness;
using APIPix.Business.UsuarioBusiness;
using APIPix.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace APIPix.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<APIPixContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("APIPixConnectionString")));
            
            services.AddScoped<UsuarioBusiness>();
            services.AddScoped<ChavePixBusiness>();

            return services;
        }
    }
}
