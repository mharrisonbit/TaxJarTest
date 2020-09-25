using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using TaxJarTest.Helpers;
using TaxJarTest.Interfaces;

namespace TaxJarTest.RestCalls
{
    public class GetDataCalls : IGetDataCalls
    {
        public async Task<string> GetDataFromApi(JObject callData)
        {
            var details = "";
            var apiUrl = Constants.ApiUrl;
            var urlString = $"{apiUrl}rates/{callData["zipcode"]}?country={callData["country"]}&city={callData["city"]}";
            var authToken = Constants.ApiKey;
            var method = new HttpMethod("GET");
            var request = new HttpRequestMessage(method, urlString) { };

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Token token=\"{authToken}\"");

            var response = await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (result, timeSpan, retryCount, context) => { })
                .ExecuteAsync(() => httpClient.SendAsync(request));

            if (response.IsSuccessStatusCode)
            {
                details = await response.Content.ReadAsStringAsync();
            }
            response.Dispose();
            httpClient.Dispose();

            return details;
        }

        public async Task<string> GetTaxForOrderFromApi(JObject orderInfo)
        {
            var details = "";

            var authToken = Constants.ApiKey;
            var method = new HttpMethod("POST");
            var body = JsonConvert.SerializeObject(orderInfo);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(method, $"{Constants.ApiUrl}taxes")
            {
                Content = content
            };

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Token token=\"{authToken}\"");
            var response = await Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (result, timeSpan, retryCount, context) =>
                {
                })
                .ExecuteAsync(() => httpClient.SendAsync(request));

            if (response.IsSuccessStatusCode)
            {
                details = await response.Content.ReadAsStringAsync();
            }
            response.Dispose();
            request.Dispose();
            httpClient.Dispose();

            return details;
        }
    }
}
