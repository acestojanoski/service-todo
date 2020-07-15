using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceTodo.Api.Models.Request;
using ServiceTodo.Domain.Entities;

namespace ServiceTodo.Api.Services
{
    public interface ITodoService
    {
        Task<Todo> GetTodoById(int id);
        Task<IEnumerable<Todo>> GetTodos(GetTodosRequest request);
    }
}
