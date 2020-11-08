using AutoMapper;
using TodoListStart.Application.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        private IRepository _repo;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _services;
        public DataHelper(IServiceProvider services)
        {
            _services = services;
            _mapper = _services.GetRequiredService<IMapper>();
        }
    }
}
