using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using System.Linq;
using AutoMapper.Internal;
using TodoListStart.Application.Controllers;
using TodoListStart.Application.Constants;
using FluentValidation.Results;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Constants;
using TodoListStart.IntegrationTests.Support.Facade;
using TodoListStart.Application.Models.Auth;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        public RequestResult<bool> Registration(RegistrationUser userInfo)
        {
            var jsonData = JsonSerializer.Serialize(userInfo);
            var builder = BuildRequest(Urls.REGISTRATION);
            builder.And(request => request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json"));
            var response = builder.PostAsync().GetAwaiter().GetResult();
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                UpdateCookies(response);
                return new RequestResult<bool>(true);
            }
            else
            {
                return new RequestResult<bool>().AddError("Any Error");
            }
        }
        public RequestResult<bool> SignIn(UserSignIn signIn)
        {
            var jsonData = JsonSerializer.Serialize(signIn);
            var builder = BuildRequest(Urls.SIGNIN);
            builder.And(request => request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json"));
            var response = builder.PostAsync().GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                UpdateCookies(response);
                return new RequestResult<bool>(true);
            }
            else
            {
                return new RequestResult<bool>().AddError("Any Error");
            }
        }
        public RequestResult<bool> SignOut()
        {
            var response = BuildRequest(Urls.SIGNOUT).GetAsync().GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                UpdateCookies(response);
                return new RequestResult<bool>(true);
            }
            else
            {
                return new RequestResult<bool>().AddError("Any Error");
            }
        }
    }
}
