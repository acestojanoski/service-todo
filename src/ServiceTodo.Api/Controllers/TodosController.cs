using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceTodo.Api.Models.Request;
using ServiceTodo.Api.Services;
using ServiceTodo.Domain.Entities;
using ServiceTodo.Domain.Interfaces;

namespace ServiceTodo.Api.Controllers
{
    [Route("api/{controller}")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoService _todoService;

        public TodosController(ITodoRepository todoRepository, ITodoService todoService)
        {
            _todoRepository = todoRepository;
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] GetTodosRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<Todo> todos = await _todoService.GetTodos(request);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById([FromRoute] int id)
        {
            Todo todo = await _todoService.GetTodoById(id);
            return Ok(todo);
        }
    }
}
