using Blog.Codewars.Generator.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blog.Codewars.Generator
{
    public static class Source
    {
        private static readonly HttpClient Client = new HttpClient();

        private static async ValueTask<Completion> AsCompletion(Completed.Datum source, HttpClient client)
        {
            var response = await client.GetAsync($"https://www.codewars.com/api/v1/code-challenges/{source.id}");

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var challenge = await JsonSerializer.DeserializeAsync<Challenges.Rootobject>(stream);

                return new Completion
                {
                    Date = source.completedAt,
                    Name = challenge.name,
                    Uri = challenge.url,
                    Language = source.completedLanguages.FirstOrDefault(),
                    Colour = challenge.rank.color,
                    Ktu = challenge.rank.name,
                    Tags = challenge.tags
                };
            }
        }

        private static async Task<Completed.Rootobject> Completed(HttpClient client)
        {
            var completedResponse = await client.GetAsync("https://www.codewars.com/api/v1/users/ibebbs/code-challenges/completed");

            using (var stream = await completedResponse.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<Completed.Rootobject>(stream);
            }
        }

        private static async Task<IEnumerable<Completion>> Completions(HttpClient client, int numberOfCompletionstoInclude)
        {
            var completed = await Completed(client);

            var result = await completed.data
                .ToAsyncEnumerable()
                .SelectAwait(d => AsCompletion(d, client))
                .Take(numberOfCompletionstoInclude)
                .ToArrayAsync();

            return result;
        }

        private static async Task<Profile.Rootobject> Profile(HttpClient client)
        {
            var completedResponse = await client.GetAsync("https://www.codewars.com/api/v1/users/ibebbs/");

            using (var stream = await completedResponse.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<Profile.Rootobject>(stream);
            }
        }

        public static async Task<Model> Create(HttpClient client, int numberOfCompletionstoInclude)
        {
            var profile = await Profile(client);
            var completions = await Completions(client, numberOfCompletionstoInclude);

            return new Model
            {
                Honor = profile.honor,
                LeaderboardPosition = profile.leaderboardPosition,
                TotalCompleted = profile.codeChallenges.totalCompleted,
                Completions = completions
            };
        }

        public static Task<Model> Create(int numberOfCompletionstoInclude)
        {
            return Create(Client, numberOfCompletionstoInclude);
        }
    }
}
