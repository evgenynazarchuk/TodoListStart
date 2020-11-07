using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using TodoListStart.Application.Constants;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.TestHost;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        private static JsonSerializerOptions JsonOptionCamelCase = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        private readonly TestServer _server;
        private CookieContainer _cookieContainer { get; }
        public FacadeHelper(TestServer testServer)
        {
            _server = testServer;
            _cookieContainer = new CookieContainer();
        }
        private RequestBuilder BuildRequest(string path)
        {
            var uri = new Uri(Urls.HOST);
            var builder = _server.CreateRequest(path);

            var cookieHeader = _cookieContainer.GetCookieHeader(uri);
            if (!string.IsNullOrWhiteSpace(cookieHeader))
            {
                builder.AddHeader(HeaderNames.Cookie, cookieHeader);
            }
            return builder;
        }

        private void UpdateCookies(HttpResponseMessage response)
        {
            if (response.Headers.Contains(HeaderNames.SetCookie))
            {
                var uri = new Uri(Urls.HOST);
                var cookies = response.Headers.GetValues(HeaderNames.SetCookie);
                foreach (var cookie in cookies)
                {
                    _cookieContainer.SetCookies(uri, cookie);
                }
            }
        }

        private RequestResult<TType> GetRequest<TType>(string path)
        {
            var response = BuildRequest(path).GetAsync().GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new RequestResult<TType>().AddError(ErrorMessages.InternalServerError);
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new RequestResult<TType>().AddError(ErrorMessages.NotFound);
            }
            #endregion catch_error

            UpdateCookies(response);
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var obj = JsonSerializer.Deserialize<TType>(content, JsonOptionCamelCase);
            return new RequestResult<TType>(obj);
        }
        private RequestResult<TType> PostRequest<TType>(string path, TType obj)
        {
            var jsonData = JsonSerializer.Serialize(obj);
            var builder = BuildRequest(path);
            builder.And(request => request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json"));
            var response = builder.PostAsync().GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new RequestResult<TType>().AddError(ErrorMessages.InternalServerError);
            }

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorList = JsonSerializer.Deserialize<List<string>>(content, JsonOptionCamelCase);
                return new RequestResult<TType>().AddErrors(errorList);
            }
            #endregion catch_error

            UpdateCookies(response);
            var responseObj = JsonSerializer.Deserialize<TType>(content, JsonOptionCamelCase);
            return new RequestResult<TType>(responseObj);
        }
        private RequestResult<bool> PutRequest<TType>(string path, TType obj)
        {
            var jsonData = JsonSerializer.Serialize(obj);
            var builder = BuildRequest(path);
            builder.And(request => request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json"));
            var response = builder.SendAsync("PUT").GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new RequestResult<bool>().AddError(ErrorMessages.InternalServerError);
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new RequestResult<bool>().AddError(ErrorMessages.NotFound);
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var errorList = JsonSerializer.Deserialize<List<string>>(content, JsonOptionCamelCase);
                return new RequestResult<bool>().AddErrors(errorList);
            }
            #endregion catch_error

            UpdateCookies(response);
            return new RequestResult<bool>(true);
        }
        private RequestResult<bool> DeleteRequest(string path)
        {
            var builder = BuildRequest(path);
            var response = builder.SendAsync("DELETE").GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new RequestResult<bool>().AddError(ErrorMessages.InternalServerError);
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new RequestResult<bool>().AddError(ErrorMessages.NotFound);
            }
            #endregion catch_error

            UpdateCookies(response);
            return new RequestResult<bool>(true);
        }
    }
}
