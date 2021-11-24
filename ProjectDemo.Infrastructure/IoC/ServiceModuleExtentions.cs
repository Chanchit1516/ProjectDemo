using Microsoft.Extensions.DependencyInjection;
using ProjectDemo.Core.Interfaces.Repositories;
using ProjectDemo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo.Infrastructure.IoC
{
    public static class ServiceModuleExtentions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
