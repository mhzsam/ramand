using Infrastructure.Dapper;
using Infrastructure.Repository;
using Infrastructure.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DIRegister
{
    public static class DIRegister
    {
        public static void AddInfrastractureDI(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
