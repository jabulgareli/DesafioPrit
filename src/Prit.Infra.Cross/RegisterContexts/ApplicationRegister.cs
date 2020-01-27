using Microsoft.Extensions.DependencyInjection;
using Prit.Application.Interfaces;
using Prit.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prit.Infra.Cross.RegisterContexts
{
    public class ApplicationRegister
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IProductAppService, ProductAppService>();
        }
    }
}
