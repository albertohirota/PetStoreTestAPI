using RestSharp;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System.Net;

namespace PetStoreAPI
{
    public  class ApiMockHelper
    {
        private readonly ApiHelper apiHelper = new();
        private const string baseURL = "https://petstore.swagger.io/";

        public RestClient SetMockUrl(string url, HttpStatusCode code, HttpMethod method, string apiMethod)
        {
            var mockHttp = GetMockHttp(code, method, url, apiMethod);
            var client = new RestClient(new RestClientOptions { ConfigureMessageHandler = _ => mockHttp });
            return client;
        }

        public MockHttpMessageHandler GetMockHttp(HttpStatusCode code, HttpMethod method, string url, string apiMethod)
        {
            var mockHttp = new MockHttpMessageHandler();
            switch (apiMethod)
            {
                case "Get":
                    // Setup a respond for the user api (including a wildcard in the URL)
                    mockHttp.When(method, url)
                        .Respond(code); // Respond with JSON "application/json", "{'name' : 'Test McGee'}" //
                    break;
                case "Post":
                    mockHttp
                        .When(method, url)
                        .Respond(code, "application/json", "{'status' : 'Boo'}");
                    break;
                case "Put":
                    mockHttp
                        .When(method, url)
                        .Respond(code, "application/json", "{'status' : 'Boo'}");
                    break;
                case "Delete":
                    mockHttp
                        .When(method, url)
                        .Respond(code, "application/json", "{'status' : 'Boo'}");
                    break;

                default: break;
            }

            return mockHttp;
        }

        public async Task<RestResponse> GetMockResponse(string apiUrl, HttpStatusCode code)
        {
            var url = Path.Combine(baseURL, apiUrl);
            var client = SetMockUrl(url, code, HttpMethod.Get, "Get"); 
            return await apiHelper.GetResponse(url, client);
        }

        public async Task<RestResponse> PostMockResponse(string apiUrl, HttpStatusCode code)
        {
            var obj = apiHelper.GetPetJsonObject(9999, "dog", "Louis");
            var jsonResult = JsonConvert.SerializeObject(obj);
            var url = Path.Combine(baseURL, apiUrl);
            var client = SetMockUrl(url, code, HttpMethod.Post, "Post");
            return await apiHelper.PostResponse(url, client, jsonResult);
        }

        public async Task<RestResponse> PutMockResponse(string apiUrl, HttpStatusCode code)
        {
            var url = Path.Combine(baseURL, apiUrl);
            var client = SetMockUrl(url, code, HttpMethod.Put, "Put");
            return await apiHelper.PutResponse(url, client);
        }

        public async Task<RestResponse> DeleteMockResponse(string apiUrl, HttpStatusCode code)
        {
            var url = Path.Combine(baseURL, apiUrl);
            var client = SetMockUrl(url, code, HttpMethod.Delete, "Delete");
            return await apiHelper.DeleteResponse(url, client);
        }
    }
}
