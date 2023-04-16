using RestSharp;
using PetStoreAPI.Models;
using System.Security.Cryptography.X509Certificates;
using RestSharp.Authenticators;
using System.ComponentModel;

namespace PetStoreAPI
{
    public class ApiHelper
    {
        private RestRequest? request;
        private const string baseURL = "https://petstore.swagger.io/";

        public RestClient SetUrl(string endPoint, string userName="", string passw="")
        {
            var url = Path.Combine(baseURL, endPoint);
            var options = new RestClientOptions(url)
            {
                Authenticator = new HttpBasicAuthenticator(userName, passw)
            };

            var client = new RestClient();
            return (userName =="")? new RestClient(): new RestClient(options);
        }

        public RestRequest CreateRequest(Method type, string url = "")
        {
            request = new RestRequest(url)
            {
                Method = type,
                RequestFormat = DataFormat.Json
            };
            request.AddHeaders(new Dictionary<string, string>
            {
                {"Accept","application/json" },{"Content-Type","application/json" }
            } );

            return request;
        }

        public async Task<RestResponse> GetResponse(string url, RestClient client)
        {
            var request = CreateRequest(Method.Get, url);
            var response = await client.ExecuteAsync(request); 
            return response;
        }

        public async Task<RestResponse> PostResponse(string url, RestClient client, string json = "")
        {
            var request = CreateRequest(Method.Post, url);
            request.AddBody(json);
            var response = await client.ExecutePostAsync(request); 
            return response;
        }

        public async Task<RestResponse> PutResponse(string url, RestClient client, string json = "")
        {
            var request = CreateRequest(Method.Put, url);
            request.AddBody(json);
            var response = await client.ExecutePutAsync(request);
            return response;
        }

        public async Task<RestResponse> DeleteResponse(string url, RestClient client)
        {
            var request = CreateRequest(Method.Delete, url);
            var response = await client.ExecuteAsync(request);
            return response;
        }

        public Pets GetPetJsonObject(int idNumber, string categoryName, string petName)
        {
            var category = new Category
            {
                id = 1,
                name = categoryName
            };

            var tag = new Tag
            {
                id = 1,
                name = "tagString"
            };
            List<Tag> tagList = new()
            {
                tag
            };

            List<string> photoList = new()
            {
                "photoString"
            };

            var pet = new Pets
            {
                id = idNumber,
                category = category,
                name = petName,
                photoUrls = photoList,
                tags = tagList,
                status = "available"
            };
            return pet;
        }

        public Stores GetStoreJsonObject(int idNumber, int quant)
        {
            var store = new Stores
            {
                id = idNumber,
                petId = 1,
                quantity = quant,
                shipDate = DateTime.Now,
                status = "placed",
                complete = true
            };
            return store;
        }

        public Users GetUserJsonObject(int idNumber, string uName, string fName, string lName, string passw)
        {
            var store = new Users
            {
                id = idNumber,
                username= uName,
                firstName= fName,
                lastName= lName,
                email= "hello.world@gmail.com",
                password= passw,
                phone= "613-713-8139",
                userStatus = 1
            };
            return store;
        }
    }
}