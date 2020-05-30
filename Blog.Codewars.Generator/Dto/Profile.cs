using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Codewars.Generator.Dto
{
    public static class Profile
    {

        public class Rootobject
        {
            public string username { get; set; }
            public string name { get; set; }
            public int honor { get; set; }
            public string clan { get; set; }
            public int leaderboardPosition { get; set; }
            public object[] skills { get; set; }
            public Ranks ranks { get; set; }
            public Codechallenges codeChallenges { get; set; }
        }

        public class Ranks
        {
            public Overall overall { get; set; }
            public Languages languages { get; set; }
        }

        public class Overall
        {
            public int rank { get; set; }
            public string name { get; set; }
            public string color { get; set; }
            public int score { get; set; }
        }

        public class Languages
        {
            public Fsharp fsharp { get; set; }
            public Csharp csharp { get; set; }
        }

        public class Fsharp
        {
            public int rank { get; set; }
            public string name { get; set; }
            public string color { get; set; }
            public int score { get; set; }
        }

        public class Csharp
        {
            public int rank { get; set; }
            public string name { get; set; }
            public string color { get; set; }
            public int score { get; set; }
        }

        public class Codechallenges
        {
            public int totalAuthored { get; set; }
            public int totalCompleted { get; set; }
        }

    }
}
