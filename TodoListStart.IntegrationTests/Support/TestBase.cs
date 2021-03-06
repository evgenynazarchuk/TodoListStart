﻿using System;
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
        private readonly AppDbContext _db;
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _date;
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
            _db = _server.Host.Services.GetRequiredService<AppDbContext>();
            _repo = _server.Host.Services.GetRequiredService<IRepository>();
            _date = _server.Host.Services.GetRequiredService<IDateTimeService>();
            _mapper = _server.Host.Services.GetRequiredService<IMapper>();

            Facade = new FacadeHelper(_client);
            Data = new DataHelper(_repo, _mapper);
            Date = new DateHelper(_date);
        }

        public void Dispose()
        {
            _db.Database.EnsureDeleted();
            _server.Dispose();
            _client.Dispose();
        }
    }
}
