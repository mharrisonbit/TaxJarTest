using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using TaxJarTest.Helpers;
using TaxJarTest.Interfaces;

namespace TaxJarTest.RestCalls
{
    public class GetDataCalls : IGetDataCalls
    {
        public async Task<string> GetDataFromApi()
        {
            var apiUrl = Constants.ApiUrl;
            var authToken = Constants.ApiKey;
            var method = new HttpMethod("GET");
            var request = new HttpRequestMessage(method, $"{apiUrl}/") { };

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authToken}");

            var response = await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (result, timeSpan, retryCount, context) => { })
                .ExecuteAsync(() => httpClient.SendAsync(request));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ToString());
            }
            response.Dispose();

            return "";
        }
    }
}
