using System;
using System.Collections.Generic;

namespace Blog.Codewars.Generator
{
    public class Completion
    {
        public DateTimeOffset Date { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string Language { get; set; }

        public string Colour { get; set; }

        public string Ktu { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
