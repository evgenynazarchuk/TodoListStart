using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using TodoListStart.Application.Services;
using TodoListStart.Application.Interfaces;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            // get errors
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new ResponseResult<TType>(title: "Not Found");
            }
            // end get errors

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
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // get errors
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = JsonSerializer.Deserialize<ResponseError>(content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var totalErrorMessage = new List<string>();
                foreach (var modelState in errors.Errors)
                {
                    totalErrorMessage.AddRange(modelState.Value);
                }
                return new ResponseResult<TType>(errors.Title, totalErrorMessage);
            }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return new ResponseResult<TType>(title: "Internal Server Error");
            }
            // end get errors

            // return correct obj
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

            // get errors
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var errors = JsonSerializer.Deserialize<ResponseError>(content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                return new ResponseResult<bool>(title: errors.Title);
            }
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errors = JsonSerializer.Deserialize<ResponseError>(content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var totalErrorMessage = new List<string>();
                foreach (var modelState in errors.Errors)
                {
                    totalErrorMessage.AddRange(modelState.Value);
                }
                return new ResponseResult<bool>(errors.Title, totalErrorMessage);
            }
            // end get errors

            return new ResponseResult<bool>(true);
        }
        private ResponseResult<bool> DeleteRequest(string url)
        {
            var response = _client.DeleteAsync(url).GetAwaiter().GetResult();

            // get errors
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var errors = JsonSerializer.Deserialize<ResponseError>(content, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                return new ResponseResult<bool>(title: errors.Title);
            }
            // end get errors

            return new ResponseResult<bool>(true);
        }
    }
}
