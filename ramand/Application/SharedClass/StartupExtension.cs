using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass
{
    public static class StartupExtension
    {
        public static void AddCommonService(this IServiceCollection services, IConfiguration configuration)
        {
         
            services.Configure<RabbitMqConfiguration>(a => configuration.GetSection("RabbitMqConfiguration").GetChildren());
            services.AddSingleton<IRabbitMqService, RabbitMqService>();
        }
    }
}
