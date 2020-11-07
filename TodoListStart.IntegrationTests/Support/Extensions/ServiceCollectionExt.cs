using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Interfaces;
using TodoListStart.Application;
using TodoListStart.IntegrationTests.Support.Services;
using Microsoft.AspNetCore.Http;
using System;

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
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);

            return services;
        }
        public static IServiceCollection SwapUserService(this IServiceCollection services)
        {
            return services.AddSingleton<IUserService, UserServiceMock>();
        }
    }
}
