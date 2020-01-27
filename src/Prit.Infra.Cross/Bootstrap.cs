using Microsoft.Extensions.DependencyInjection;
using Prit.Infra.Cross.RegisterContexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prit.Infra.Cross
{
    public static class Bootstrap
    {
        public static void RegisterAllServices(IServiceCollection services)
        {
            ApplicationRegister.Register(services);
            InfraRegister.Register(services);
        }
    }
}
