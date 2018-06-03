using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (!_context.TodoItems.Any())
            {
                _context.TodoItems.Add(new TodoItem { Name = "Tatsuya" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null) return NotFound();

            return Ok();
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null) return BadRequest();

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new TodoItem { Id = item.Id }, item);
        }

        // [HttpPut("{id}")]
        // public IActionResult Update(long id, [FromBody] TodoItem item)
        // {
        //     if (item == null || item.Id != id)
        //     {
        //         return BadRequest();
        //     }

        //     var todo = _context.TodoItems.Find(id);
        //     if (todo == null)
        //     {
        //         return NotFound();
        //     }

        //     todo.IsComplete = item.IsComplete;
        //     todo.Name = item.Name;

        //     _context.TodoItems.Update(todo);
        //     _context.SaveChanges();
        //     return NoContent();
        // }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TodoItem item)
        {
            if (item?.Id != id) return BadRequest();

            var todo = _context.TodoItems.Find(id);
            if (todo == null) return NotFound();

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.TodoItems.Find(id);
            if(todo == null) return NotFound();

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}