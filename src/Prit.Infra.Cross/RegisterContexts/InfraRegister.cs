using Microsoft.Extensions.DependencyInjection;
using Prit.Domain.Interfaces.Repositories;
using Prit.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prit.Infra.Cross.RegisterContexts
{
    public class InfraRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductSqlRepository>();
        }
    }
}
