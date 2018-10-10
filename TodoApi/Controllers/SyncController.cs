using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Models;

namespace SyncController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMyDependency _myDependency;
       

        public SyncController(IConfiguration configuration, IMyDependency myDependency)
        {

            _configuration = configuration;
            _myDependency = myDependency;

        }

        [HttpGet("invoke")]
        public async Task Invoke([FromServices] IFileProvider fileProvider)
        {
            IFileInfo file = fileProvider.GetFileInfo("Models/jschema/json-schema.json");

            using (var stream = file.CreateReadStream())
            using (var reader = new StreamReader(stream))
            {
                var output = await reader.ReadToEndAsync();
                await HttpContext.Response.WriteAsync(output.ToString());
            }
        }


        [HttpGet("sync")]
        public IActionResult SyncGet()
        {
            _myDependency.WriteMessage("test");
            return Ok();
        }

        [HttpGet("async")]
        public async Task<IActionResult> AsyncGet()
        {
            Console.WriteLine("TESTD" + Thread.CurrentThread.ManagedThreadId);
            return Ok(await AccessTheWebAsync());
        }

       
        // Three things to note in the signature:  
        //  - The method has an async modifier.   
        //  - The return type is Task or Task<T>. (See "Return Types" section.)  
        //    Here, it is Task<int> because the return statement returns an integer.  
        //  - The method name ends in "Async."  
        async Task<int> AccessTheWebAsync()
        {
            Console.WriteLine("TESTA" + Thread.CurrentThread.ManagedThreadId);

            // GetStringAsync returns a Task<string>. That means that when you await the  
            // task you'll get a string (urlContents).  
            Task<int> nummer = GetNumberAsync(1);
 
            // You can do work here that doesn't rely on the string from GetStringAsync.  
            Console.WriteLine("TESTB"+Thread.CurrentThread.ManagedThreadId);

            // The await operator suspends AccessTheWebAsync.  
            //  - AccessTheWebAsync can't continue until getStringTask is complete.  
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.  
            //  - Control resumes here when getStringTask is complete.   
            //  - The await operator then retrieves the string result from getStringTask.  
            Console.WriteLine("TEST-STATUS" + nummer.Status);
            int urlContents = await nummer;
            Console.WriteLine("TEST-STATUS" + nummer.Status);
            Console.WriteLine("TESTC" + Thread.CurrentThread.ManagedThreadId);
            // The return statement specifies an integer result.  
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.  
            return urlContents;
        }

        public async Task<int> GetNumberAsync(int number)
        {
            await Task.Delay(1000);

            if (number < 0)
            {
                throw new ArgumentException();
            }

            return number;
        }

    }

}