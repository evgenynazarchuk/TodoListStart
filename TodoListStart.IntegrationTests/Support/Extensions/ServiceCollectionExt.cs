using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using System.Runtime.CompilerServices;
using TodoListStart.Application.Interfaces;
using TodoListStart.Application;
using TodoListStart.IntegrationTests.Support.Services;

namespace TodoListStart.IntegrationTests.Support.Extensions
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection SwapDateTimeService(this IServiceCollection services)
        {
            return services.AddSingleton<IDateTimeService, DateTimeServiceMock>();
        }
        public static IServiceCollection SwapDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseInMemoryDatabase("db");
                options.UseSqlite("data source=testdb");
            }/*, ServiceLifetime.Transient, ServiceLifetime.Transient*/);

            return services;
        }
    }
}
