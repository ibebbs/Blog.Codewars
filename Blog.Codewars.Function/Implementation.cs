using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Storage.Blob;
using System.Linq;

namespace Blog.Codewars.Function
{
    public static class Implementation
    {
        private static bool IsCodeWars(HttpRequest request)
        {
            return request.Headers.TryGetValue("X-Webhook-Secret", out var values) && values.Contains(Settings.CodewarsSecret);
        }

        private static async Task<bool> IsMyHonorChange(HttpRequest request, ILogger log)
        {
            using (StreamReader reader = new StreamReader(request.Body))
            {
                var body = await reader.ReadToEndAsync();

                log.LogInformation($"Body: '{body}'");

                return body.Contains("honor_changed") && body.Contains(Settings.MyCodewarsId);
            }
        }

        [FunctionName("WebHook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
            [Blob("blog/codewars.html", FileAccess.Write, Connection = "AzureWebJobsStorage")] CloudBlockBlob output,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (IsCodeWars(request))
            {
                if (await IsMyHonorChange(request, log))
                {
                    var content = await Generator.Implementation.GenerateBlogPage(Settings.NumberOfCompletionstoInclude);
                    output.Properties.ContentType = "text/html";
                    await output.UploadTextAsync(content);

                    return new NoContentResult();
                }
                else
                {
                    return new StatusCodeResult(304);
                }
            }
            else
            {
                return new UnauthorizedResult();
            }
        }

        [FunctionName("WebHookDebug")]
        public static async Task<IActionResult> RunDebug(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest request,
            [Blob("blog/codewars.txt", FileAccess.Write, Connection = "AzureWebJobsStorage")] CloudBlockBlob output,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            log.LogInformation("Headers:");
            foreach (var header in request.Headers)
            {
                log.LogInformation($"{header.Key}={string.Join(',', header.Value)}");
            }

            var query = request.QueryString.HasValue
                ? request.QueryString.Value
                : string.Empty;

            log.LogInformation($"QueryString: '{query}'");

            using (StreamReader reader = new StreamReader(request.Body))
            {
                var body = await reader.ReadToEndAsync();
                log.LogInformation($"Body: '{body}'");
            }

            return new OkResult();
        }
    }
}
