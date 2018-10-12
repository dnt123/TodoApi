using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
    }

    public class Album
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }
    }
}
