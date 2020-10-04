using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Text.Json;

namespace TodoListStart.IntegrationTests.Support.Facade
{
    public partial class FacadeHelper
    {
        private readonly HttpClient _client;
        public FacadeHelper(HttpClient client)
        {
            _client = client;
        }
        private TType GetRequest<TType>(string url)
        {
            var response = _client.GetAsync(url).GetAwaiter().GetResult();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var obj = JsonSerializer.Deserialize<TType>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return obj;
        }
        private TType PostRequest<TType>(string url, TType obj)
        {
            var jsonData = JsonSerializer.Serialize(obj);
            var response = _client.PostAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
            var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var responseObj = JsonSerializer.Deserialize<TType>(content, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return responseObj;
        }
        private HttpResponseMessage PutRequest<TType>(string url, TType obj)
        {
            var jsonData = JsonSerializer.Serialize<TType>(obj);
            var response = _client.PutAsync(url, new StringContent(jsonData, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
            return response;
        }
        private HttpResponseMessage DeleteRequest(string url)
        {
            var response = _client.DeleteAsync(url).GetAwaiter().GetResult();
            return response;
        }
    }
}
