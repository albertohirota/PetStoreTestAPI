using RestSharp;
using RestSharp.Authenticators;
using System.Net;


namespace PetStoreAPI
{
    public class ApiCalls
    { 
        private readonly ApiHelper apiHelper = new();
        private readonly ApiMockHelper mockHelper = new();
        private const string baseURL = "https://petstore.swagger.io/";

        public async Task<RestResponse> GetApiContent(string getApiUrl, string userName = "", string passw = "")
        {
            var client = apiHelper.SetUrl(getApiUrl, userName, passw);
            var url = Path.Combine(baseURL, getApiUrl);
            var response = await apiHelper.GetResponse(url,client);       
            return response;
        }

        public async Task<RestResponse> PostContent(string getApiUrl, string json, string userName = "", string passw = "")
        {
            var client = apiHelper.SetUrl(getApiUrl, userName, passw);
            var url = Path.Combine(baseURL, getApiUrl);
            var response = await apiHelper.PostResponse(url, client, json );
            return response;
        }

        public async Task<RestResponse> PutContent(string getApiUrl, string json, string userName = "", string passw = "")
        {
            var client = apiHelper.SetUrl(getApiUrl, userName, passw);
            var url = Path.Combine(baseURL, getApiUrl);
            var response = await apiHelper.PutResponse(url, client, json);
            return response;
        }

        public async Task<RestResponse> DeleteContent(string getApiUrl, string userName = "", string passw = "")
        {
            var client = apiHelper.SetUrl(getApiUrl, userName, passw);
            var url = Path.Combine(baseURL, getApiUrl);
            var response = await apiHelper.DeleteResponse(url, client);
            return response;
        }

        public async Task<int> GetStatusCodeMockApi(string getApiUrl, HttpStatusCode httpCode)
        {
            var response = await mockHelper.GetMockResponse(getApiUrl, httpCode);
            var code = (int) response.StatusCode;
            return code;
        }

        public async Task<int> PostStatusCodeMockApi(string getApiUrl, HttpStatusCode httpCode)
        {
            var response = await mockHelper.PostMockResponse(getApiUrl, httpCode);
            var code = (int)response.StatusCode;
            return code;
        }

        public async Task<int> PutStatusCodeMockApi(string getApiUrl, HttpStatusCode httpCode)
        {
            var response = await mockHelper.PutMockResponse(getApiUrl, httpCode);
            var code = (int)response.StatusCode;
            return code;
        }

        public async Task<int> DeleteStatusCodeMockApi(string getApiUrl, HttpStatusCode httpCode)
        {
            var response = await mockHelper.DeleteMockResponse(getApiUrl, httpCode);
            var code = (int)response.StatusCode;
            return code;
        }
    }
}
