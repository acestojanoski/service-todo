using ServiceTodo.Domain.Interfaces;
using RestSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using ServiceTodo.Domain.Entities;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ServiceTodo.Integration.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IRestClient _client;
        private readonly IConfiguration _configuration;

        public TodoRepository(IRestClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<Todo> GetTodoById(int id)
        {
            string url = $"{_configuration["ServiceTodo:TodosUrl"]}/{id}";
            IRestRequest request = new RestRequest(url, Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Todo>(response.Content);
        }

        public async Task<IEnumerable<Todo>> GetTodos()
        {
            IRestRequest request = new RestRequest(_configuration["ServiceTodo:TodosUrl"], Method.GET);
            IRestResponse response = await _client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<IEnumerable<Todo>>(response.Content);
        }
    }
}
