using System;
using TodoListStart.IntegrationTests.Support.Facade;
using TodoListStart.IntegrationTests.Support.Data;
using Microsoft.AspNetCore.TestHost;
using TodoListStart.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using TodoListStart.IntegrationTests.Support.Extensions;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.IntegrationTests.Support
{
    public class TestBase : IDisposable
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;
        private readonly AppDbContext _db;
        public readonly FacadeHelper Facade;
        public readonly DataHelper Data;
        public readonly DateHelper Date;
        public readonly AuthHelper Auth;
        
        public TestBase()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.SwapDbContext();
                })
                .ConfigureTestServices(services =>
                {
                    services.SwapDateTimeService();
                    services.SwapUserService();
                }));

            _client = _server.CreateClient();
            _db = _server.Host.Services.GetRequiredService<AppDbContext>();
            var dateService = _server.Host.Services.GetRequiredService<IDateTimeService>();

            Facade = new FacadeHelper(_server);
            Data = new DataHelper(_server.Host.Services);
            Date = new DateHelper(dateService);
            Auth = new AuthHelper(_server.Host.Services);
        }
        public void Dispose()
        {
            _db.Database.EnsureDeleted();
            _server.Dispose();
            _client.Dispose();
        }
    }
}
