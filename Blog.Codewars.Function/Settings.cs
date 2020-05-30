using System;

namespace Blog.Codewars.Function
{
    public static class Settings
    {
        public static string CodewarsSecret => Environment.GetEnvironmentVariable("CodewarsSecret");

        public static string MyCodewarsId => Environment.GetEnvironmentVariable("MyCodewarsId");

        public static int NumberOfCompletionstoInclude => Int32.Parse(Environment.GetEnvironmentVariable("NumberOfCompletionstoInclude"));
    }
}
