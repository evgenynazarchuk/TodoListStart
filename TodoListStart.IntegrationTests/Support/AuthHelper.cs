using Microsoft.AspNetCore.Identity;
using System;
using TodoListStart.Application;
using TodoListStart.Application.Models.Auth;
using Microsoft.Extensions.DependencyInjection;
using TodoListStart.IntegrationTests.Support.Services;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.IntegrationTests.Support
{
    public class AuthHelper
    {
        private readonly IServiceProvider _services;
        private IUserService _userService;
        public AuthHelper(IServiceProvider services)
        {
            _services = services;

        }
        public IdentityResult Registration(RegistrationUser userInfo) 
        {
            var userManager = _services.GetRequiredService<UserManager<ApplicationUser>>();
            var user = new ApplicationUser { Email = userInfo.Email, UserName = userInfo.Email };
            var result = userManager.CreateAsync(user, userInfo.Password).GetAwaiter().GetResult();
            return result;
        }
        public void SetUser(string email)
        {
            _userService = _services.GetRequiredService<IUserService>();
            (_userService as UserServiceMock).SetUser(email);
        }
        public void DeleteUser(string email)
        {
            throw new ApplicationException("not implement");
        }
        public void UpdateUser(ApplicationUser user)
        {
            throw new ApplicationException("not implement");
        }
    }
}
