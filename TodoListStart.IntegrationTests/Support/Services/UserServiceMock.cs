using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TodoListStart.Application.Interfaces;
using System;

namespace TodoListStart.IntegrationTests.Support.Services
{
    public class UserServiceMock : IUserService
    {
        private IHttpContextAccessor _http;
        private IServiceProvider _services;
        private string _email;
        public string Email
        {
            get
            {
                _http = _services.GetRequiredService<IHttpContextAccessor>();
                return _http.HttpContext?.User?.Identity?.Name ?? _email;
            }
            set
            {
                _email = value;
            }
        }
        public UserServiceMock(IServiceProvider services)
        {
            _services = services;
        }
        public void SetUser(string email)
        {
            _email = email;
        }
    }
}
