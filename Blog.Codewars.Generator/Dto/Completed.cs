using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Codewars.Generator.Dto
{
    public static class Completed
    {
        public class Rootobject
        {
            public int totalPages { get; set; }
            public int totalItems { get; set; }
            public Datum[] data { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string[] completedLanguages { get; set; }
            public DateTime completedAt { get; set; }
        }
    }
}
