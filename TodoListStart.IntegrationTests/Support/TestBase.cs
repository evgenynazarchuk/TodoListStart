using System;
using TodoListStart.IntegrationTests.Support.Facade;
using TodoListStart.IntegrationTests.Support.Data;
using Microsoft.AspNetCore.TestHost;
using TodoListStart.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using TodoListStart.Application.ApplicationServices;
using TodoListStart.IntegrationTests.Support.Extensions;
using TodoListStart.Application.Interfaces;
using AutoMapper;

namespace TodoListStart.IntegrationTests.Support
{
    public class TestBase : IDisposable
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;
        private readonly AppDbContext _dbContext;
        private readonly IRepository _repoService;
        private readonly IMapper _mapperService;
        private readonly IDateTimeService _dateTimeService;
        public FacadeHelper Facade { get; set; }
        public DataHelper Data { get; set; }
        public DateHelper Date { get; set; }
        public AuthHelper Auth { get; set; }
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
                }));

            _client = _server.CreateClient();
            _dbContext = _server.Host.Services.GetRequiredService<AppDbContext>();
            _repoService = _server.Host.Services.GetRequiredService<IRepository>();
            _dateTimeService = _server.Host.Services.GetRequiredService<IDateTimeService>();
            _mapperService = _server.Host.Services.GetRequiredService<IMapper>();

            Facade = new FacadeHelper(_client);
            Data = new DataHelper(_repoService, _mapperService);
            Date = new DateHelper(_dateTimeService);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _server.Dispose();
            _client.Dispose();
        }
    }
}
