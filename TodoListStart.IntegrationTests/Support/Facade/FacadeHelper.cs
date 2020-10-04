using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.Internal;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        private readonly HttpClient _client;
        public FacadeHelper(HttpClient client)
        {
            _client = client;
        }
        private ResponseResult<TType> GetRequest<TType>(string url)
        {
            var response = _client.GetAsync(url).GetAwaiter().GetResult();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new ResponseResult<TType>(title: "Not Found");
            }
            #endregion catch_error

            var obj = JsonSerializer.Deserialize<TType>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return new ResponseResult<TType>(obj);
        }
        private ResponseResult<TType> PostRequest<TType>(string url, TType obj)
        {
            var jsonData = JsonSerializer.Serialize(obj);
            var response = _client.PostAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new ResponseResult<TType>(title: "Internal Server Error");
            }

            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var modelError = JsonSerializer.Deserialize<ResponseError>(content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var totalErrorMessage = new List<string>();
                foreach (var modelState in modelError.Errors)
                {
                    totalErrorMessage.AddRange(modelState.Value);
                }
                return new ResponseResult<TType>(modelError.Title, totalErrorMessage);
            }
            #endregion catch_error

            var responseObj = JsonSerializer.Deserialize<TType>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return new ResponseResult<TType>(responseObj);
        }
        private ResponseResult<bool> PutRequest<TType>(string url, TType obj)
        {
            var jsonData = JsonSerializer.Serialize<TType>(obj);
            var response = _client.PutAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new ResponseResult<bool>(title: "Not Found");
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = JsonSerializer.Deserialize<ResponseError>(content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var totalErrorMessage = new List<string>();
                errors.Errors.ForAll(e => totalErrorMessage.AddRange(e.Value));
                return new ResponseResult<bool>(errors.Title, totalErrorMessage);
            }
            #endregion catch_error

            return new ResponseResult<bool>(true);
        }
        private ResponseResult<bool> DeleteRequest(string url)
        {
            var response = _client.DeleteAsync(url).GetAwaiter().GetResult();

            #region catch_error
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new ResponseResult<bool>(title: "Not Found");
            }
            #endregion catch_error

            return new ResponseResult<bool>(true);
        }
    }
}
