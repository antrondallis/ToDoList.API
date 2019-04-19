using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.BusinessLogic;
using ToDoList.API.Model;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoListRepository _todoListRepository;

        public TodoController(ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _todoListRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _todoListRepository.GetById(id);
            return Ok(result);
        }

        [HttpPost(Name = "CreateTodo")]
        public IActionResult Create([FromBody] TodoItem item)
        {
            var result = _todoListRepository.Create(item);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateTodo")]
        public IActionResult Update([FromBody] TodoItem item)
        {
            var result = _todoListRepository.Update(item);
            return Ok(result);
        }

        [HttpDelete(Name = "DeleteTodo")]
        public IActionResult Delete([FromBody] TodoItem item)
        {
            var result = _todoListRepository.Delete(item);
            return Ok(result);
        }
    }
}