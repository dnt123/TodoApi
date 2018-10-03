using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IConfiguration _configuration;
        string test = "xxxxxxxxxXXXXXX";

        public TodoController(TodoContext context, IConfiguration configuration)
        {

            _context = context;
            _configuration = configuration;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }


        [HttpGet("sync")]
        [Route("sync")]
        public IActionResult SyncGet()
        {
            return Ok();
        }

        [HttpGet("async")]
        [Route("async")]
        public async Task<IActionResult> AsyncGet()
        {
            Console.WriteLine("TESTD" + Thread.CurrentThread.ManagedThreadId);
            return Ok(await AccessTheWebAsync());
        }

        [HttpGet]
        [Route("isalive")]
        [Authorize]
        public ActionResult Isalive()
        {
            
            return Ok();
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("testing")]
        //[FromServices] ISession session
        public ObjectResult Testing2()
        {
            
            var pathBase = HttpContext.Request.Path;
            HttpContext.Session.Set("test", Encoding.ASCII.GetBytes("input"));
            ObjectResult o
                 = new ObjectResult(pathBase);
            //  o.Value = "skrivom";
           
            return o;
        }


        [HttpGet]
        [Produces("application/json")]
        [Route("token")]
        public ObjectResult Testing() {
            ObjectResult o
                 = new ObjectResult(GenerateToken());
          //  o.Value = "skrivom";
            return o;
        }


        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                //Kommentar
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }


        private string GenerateToken()
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                 new Claim("test", "test")
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            

            var token = new JwtSecurityToken(
             issuer: "yourdomain.com",
             audience: "yourdomain.com",
             claims: claims,
             expires: DateTime.Now.AddMinutes(30),
             signingCredentials: creds);

            return handler.WriteToken(token);
        }

        // Three things to note in the signature:  
        //  - The method has an async modifier.   
        //  - The return type is Task or Task<T>. (See "Return Types" section.)  
        //    Here, it is Task<int> because the return statement returns an integer.  
        //  - The method name ends in "Async."  
        async Task<int> AccessTheWebAsync()
        {
            Console.WriteLine("TESTA" + Thread.CurrentThread.ManagedThreadId);
            // You need to add a reference to System.Net.Http to declare client.  
            HttpClient client = new HttpClient();
           
            // GetStringAsync returns a Task<string>. That means that when you await the  
            // task you'll get a string (urlContents).  
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");
 
            // You can do work here that doesn't rely on the string from GetStringAsync.  
            Console.WriteLine("TESTB"+Thread.CurrentThread.ManagedThreadId);

            // The await operator suspends AccessTheWebAsync.  
            //  - AccessTheWebAsync can't continue until getStringTask is complete.  
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.  
            //  - Control resumes here when getStringTask is complete.   
            //  - The await operator then retrieves the string result from getStringTask.  
            string urlContents = await getStringTask;
            Console.WriteLine("TESTC" + Thread.CurrentThread.ManagedThreadId);
            test = test + 111;
            Console.WriteLine(test);
            // The return statement specifies an integer result.  
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.  
            return urlContents.Length;
        }

    }

}