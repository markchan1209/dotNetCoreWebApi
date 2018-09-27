using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotNetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoListController(TodoContext context)
        {
            this._context = context;
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Hello World" });
                _context.TodoItems.Add(new TodoItem { Name = "Hello World2" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetAll()
        {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}" , Name="GetTodo")]
        public async Task<ActionResult<TodoItem>> GetFindID(long id){
         var item = await  _context.TodoItems.FindAsync(id);
         if(item ==null)
         {
             return NotFound();
         }
         return item;
        }
    }
}