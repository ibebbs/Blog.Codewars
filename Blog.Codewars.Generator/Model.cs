using System.Collections.Generic;

namespace Blog.Codewars.Generator
{
    public class Model
    {
        public int Honor { get; set; }
        public int LeaderboardPosition { get; set; }
        public int TotalCompleted { get; set; }
        public IEnumerable<Completion> Completions { get; set; }
    }
}
