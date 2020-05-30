using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Codewars.Generator.Dto
{
    public static class Challenges
    {

        public class Rootobject
        {
            public string id { get; set; }
            public string name { get; set; }
            public string slug { get; set; }
            public string category { get; set; }
            public DateTime? publishedAt { get; set; }
            public DateTime? approvedAt { get; set; }
            public string[] languages { get; set; }
            public string url { get; set; }
            public Rank rank { get; set; }
            public DateTime createdAt { get; set; }
            public Createdby createdBy { get; set; }
            public Approvedby approvedBy { get; set; }
            public string description { get; set; }
            public int totalAttempts { get; set; }
            public int totalCompleted { get; set; }
            public int totalStars { get; set; }
            public int voteScore { get; set; }
            public string[] tags { get; set; }
            public bool contributorsWanted { get; set; }
            public Unresolved unresolved { get; set; }
        }

        public class Rank
        {
            public int id { get; set; }
            public string name { get; set; }
            public string color { get; set; }
        }

        public class Createdby
        {
            public string username { get; set; }
            public string url { get; set; }
        }

        public class Approvedby
        {
            public string username { get; set; }
            public string url { get; set; }
        }

        public class Unresolved
        {
            public int issues { get; set; }
            public int suggestions { get; set; }
        }

    }
}
