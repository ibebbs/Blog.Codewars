using Microsoft.AspNetCore.Http;
using RazorLight;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Blog.Codewars.Generator
{
    public static class Implementation
    {
        public static async Task<string> GenerateBlogPage(int numberOfCompletionstoInclude)
        {
            var engine = new RazorLightEngineBuilder()
                .SetOperatingAssembly(Assembly.GetExecutingAssembly())
                .UseEmbeddedResourcesProject(typeof(Implementation))
                .UseMemoryCachingProvider()
                .Build();

            var model = await Source.Create(numberOfCompletionstoInclude);

            string result = await engine.CompileRenderAsync("codewars", model);

            return result;
        }
    }
}
