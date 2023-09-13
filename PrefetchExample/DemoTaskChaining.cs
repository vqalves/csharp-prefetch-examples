using System.Net;

namespace PrefetchExample
{
    public class DemoTaskChaining
    {
        public static async Task Execute()
        {
            var taskUrl = RetrieveUrlAsync();

            var taskStatusCode = taskUrl.ContinueWith(async (task) => 
            {
                return await GetStatusCodeAsync(task.Result);
            }).Unwrap();

            var taskFavicon = taskUrl.ContinueWith(async (task) => 
            {
                return await HasFaviconAsync(task.Result);
            }).Unwrap();

            var statusCode = await taskStatusCode;
            var hasFavicon = await taskFavicon;
        }

        private static async Task<string> RetrieveUrlAsync()
        {
            await Task.Delay(10_000);
            return "https://google.com";
        }

        private static async Task<HttpStatusCode> GetStatusCodeAsync(string url)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Downloading " + url);
                var response = await client.GetAsync(url);
                Console.WriteLine("Returned " + url);
                return response.StatusCode;
            }
        }

        private static async Task<HttpStatusCode> HasFaviconAsync(string url)
        {
            var baseUri = new Uri(url);
            var faviconUri = new Uri(baseUri, "/favicon.ico");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(faviconUri);
                return response.StatusCode;
            }
        }
    }
}