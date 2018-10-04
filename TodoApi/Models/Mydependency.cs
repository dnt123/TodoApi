using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TodoApi.Models
{
    public class MyDependency : IMyDependency
    {
        private readonly ILogger<MyDependency> _logger;

        public MyDependency(ILogger<MyDependency> logger)
        {
            _logger = logger;
        }

        public Task WriteMessage(string message)
        {

            _logger.LogInformation(
                "MyDependency.WriteMessage called. Message: {MESSAGE}",
                message);

            return Task.FromResult(0);
        }
    }

}
