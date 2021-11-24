using Microsoft.Extensions.DependencyInjection;
using ProjectDemo.Application.Services;
using ProjectDemo.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectDemo.Application.IoC
{
    public static class ServiceModuleExtentions
    {
        public static void RegisterCoreServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IHomeService, HomeService>();
        }
    }
}
