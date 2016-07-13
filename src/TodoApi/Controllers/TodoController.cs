using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Net.Http;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        public ITodoRepository TodoItems { get; set; }
        public TodoController(ITodoRepository todoItems)
        {
            TodoItems = todoItems;
        }

        // GET: api/todo
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return TodoItems.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            var item = TodoItems.Find(id);
            // handle not found
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/todo
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            // handle bad request
            if (item == null)
            {
                return BadRequest();
            }
            TodoItems.Add(item);
            return CreatedAtRoute("GetTodo", new { controller = "Todo", id = item.Key }, item);
        }

        // PUT api/todo/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            // handle bad request
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = TodoItems.Find(id);
            // handle item not found
            if (todo == null)
            {
                return NotFound();
            }

            // update todo item
            TodoItems.Update(item);
            return new NoContentResult();
        }

        // DELETE api/todo/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TodoItems.Remove(id);
        }
    }
}
