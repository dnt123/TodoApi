using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Mediatr;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
       private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            await _mediator.Publish(new SomeEvent("Hello World"));
            return Ok("Klar");
        }
        [HttpGet("send")]
//        /api/home/send
        public async Task<IActionResult> Index2()
        {
            var result = await _mediator.Send(new Ping());
           
            return Ok(result);
        }
    }
}
