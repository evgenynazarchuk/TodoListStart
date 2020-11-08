using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Services
{
    public class UserService : IUserService
    {
        public UserService(IHttpContextAccessor http)
        {
            Email = http?.HttpContext?.User?.Identity?.Name;
        }
        public string Email { get; set; }
    }
}
