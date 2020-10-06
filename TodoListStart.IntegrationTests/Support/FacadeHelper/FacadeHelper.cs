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

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        private static JsonSerializerOptions JsonOptionCamelCase = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        private readonly HttpClient _client;
        public FacadeHelper(HttpClient client)
        {
            _client = client;
        }
        private RequestResult<TType> GetRequest<TType>(string url)
        {
            var response = _client.GetAsync(url).GetAwaiter().GetResult();

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

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var obj = JsonSerializer.Deserialize<TType>(content, JsonOptionCamelCase);
            return new RequestResult<TType>(obj);
        }
        private RequestResult<TType> PostRequest<TType>(string url, TType obj)
        {
            var jsonData = JsonSerializer.Serialize(obj);
            var response = _client
                .PostAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json"))
                .GetAwaiter().GetResult();

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

            var responseObj = JsonSerializer.Deserialize<TType>(content, JsonOptionCamelCase);
            return new RequestResult<TType>(responseObj);
        }
        private RequestResult<bool> PutRequest<TType>(string url, TType obj)
        {
            var jsonData = JsonSerializer.Serialize(obj);
            var response = _client
                .PutAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json"))
                .GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new RequestResult<bool>().AddError(ErrorMessages.InternalServerError);
            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new RequestResult<bool>().AddError(ErrorMessages.NotFound);
            }

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorList = JsonSerializer.Deserialize<List<string>>(content, JsonOptionCamelCase);
                return new RequestResult<bool>().AddErrors(errorList);
            }
            #endregion catch_error

            return new RequestResult<bool>(true);
        }
        private RequestResult<bool> DeleteRequest(string url)
        {
            var response = _client.DeleteAsync(url).GetAwaiter().GetResult();

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

            return new RequestResult<bool>(true);
        }
    }
}
