using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.Application.Models.Auth;

namespace TodoListStart.IntegrationTests.Support.Builder
{
    public class RegistrationUserBuilder
    {
        public static RegistrationUser Build()
        {
            var user = new RegistrationUser
            {
                Name = "Default Default",
                City = "Moscow",
                Email = "default@default.com",
                Password = "password",
                ConfirmPassword = "password"
            };
            return user;
        }
    }
}
