using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceTodo.Api.Exceptions;
using ServiceTodo.Api.Models.Request;
using ServiceTodo.Domain.Entities;
using ServiceTodo.Domain.Interfaces;

namespace ServiceTodo.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<Todo> GetTodoById(int id)
        {
            Todo todo = await _todoRepository.GetTodoById(id);

            if (todo == null)
            {
                throw new NotFoundException("Todo Not Found");
            }

            return todo;
        }

        public async Task<IEnumerable<Todo>> GetTodos(GetTodosRequest request)
        {
            IEnumerable<Todo> todos = await _todoRepository.GetTodos();

            if (request.PageCount == null)
            {
                return todos;
            }

            int pageNumber = request.PageNumber == null ? 0 : (int)request.PageNumber - 1;
            int skipCount = pageNumber * (int)request.PageCount;

            return todos.Skip(skipCount).Take((int)request.PageCount);
        }
    }
}
