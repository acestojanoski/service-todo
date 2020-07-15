using ServiceTodo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceTodo.Domain.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetTodos();
        Task<Todo> GetTodoById(int id);
    }
}
