using PetStoreAPI;
using Newtonsoft.Json;
using PetStoreAPI.Models;

namespace PetsTests
{
    [TestFixture]
    public class PositiveTestCases
    {
        public ApiCalls api = new();
        public ApiHelper apiHelper = new();

        [TearDown]
        public void BaseTearDown()
        {
            _ = api.DeleteContent("v2/pet/9999");
            _ = api.DeleteContent("v2/pet/9998");
            _ = api.DeleteContent("v2/pet/9997");
            _ = api.DeleteContent("v2/pet/9996");
            _ = api.DeleteContent("v2/pet/9995");
        }

        [Test]
        [TestCase("available")]
        [TestCase("pending")]
        [TestCase("sold")]
        [TestCase("returned")]
        public async Task Get200StatusCode(string status)
        {
            var response = await api.GetApiContent("v2/pet/findByStatus?status=" + status);
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }

        [Test]
        public async Task PostContent()
        {
            var obj = apiHelper.GetPetJsonObject(9997, "dog", "Louis");
            var jsonResult = JsonConvert.SerializeObject(obj);
            var response = await api.PostContent("v2/pet/", jsonResult);
            var content = response.Content;
            Pets? pet = JsonConvert.DeserializeObject<Pets>(content);
            var petName = pet?.name;
            Assert.That(petName, Is.EqualTo("Louis"));
        }

        [Test]
        public async Task PutContent()
        {
            var obj = apiHelper.GetPetJsonObject(9998, "GiantDog", "Donald");
            var jsonResult = JsonConvert.SerializeObject(obj);
            var response = await api.PutContent("v2/pet/", jsonResult);
            var content = response.Content;
            Pets? pet = JsonConvert.DeserializeObject<Pets>(content);
            var petName = pet?.name;
            Assert.That(petName, Is.EqualTo("Donald"));
        }

        [Test]
        public async Task DeleteContent()
        {
            var obj = apiHelper.GetPetJsonObject(9999, "dog", "Louis");
            var jsonResult = JsonConvert.SerializeObject(obj);
            await api.PostContent("v2/pet/", jsonResult);

            var response = await api.DeleteContent("v2/pet/9999");
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }

        [Test]
        public async Task GetContentById()
        {
            var obj = apiHelper.GetPetJsonObject(9996, "dog", "Louis");
            var jsonResult = JsonConvert.SerializeObject(obj);
            await api.PostContent("v2/pet/", jsonResult);

            var response = await api.GetApiContent("v2/pet/9996");
            var content = response.Content;
            Pets? pet = JsonConvert.DeserializeObject<Pets>(content);
            var petName = pet?.name;
            Assert.That(petName, Is.EqualTo("Louis"));
        }

        [Test]
        public async Task GetHeaderById()
        {
            var obj = apiHelper.GetPetJsonObject(9995, "dog", "Louis");
            var jsonResult = JsonConvert.SerializeObject(obj);
            await api.PostContent("v2/pet/", jsonResult);

            var response = await api.GetApiContent("v2/pet/9995");
            var content = response.Headers;
            string? contentValue = response?.Headers?.ToList()?
                .Find(x => x.Name == "Connection")?
                .Value?.ToString();
            
            Assert.That(contentValue, Is.EqualTo("keep-alive"));
        }
    }

    [TestFixture]
    public class NegativeTestCases
    {
        public ApiCalls api = new();
        public ApiHelper apiHelper = new();

        [Test]
        public async Task Get404StatusCode()
        {
            var response = await api.GetApiContent("v2/pet/findBy");
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(404));
        }

        [Test]
        public async Task Post415FailStatusCode()
        {
            var obj = apiHelper.GetPetJsonObject(9994, "dog", "Louis");
            var jsonResult = JsonConvert.SerializeObject(obj);
            var response = await api.PostContent("v2/pet/findBy", jsonResult);
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(415));
        }

        [Test]
        public async Task Put405FailStatusCode()
        {
            var obj = apiHelper.GetPetJsonObject(9993, "dog", "Louis");
            var jsonResult = JsonConvert.SerializeObject(obj);
            var response = await api.PutContent("v2/pet/findBy", jsonResult);
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(405));
        }

        [Test]
        public async Task Delete404FailToDeleteContent()
        {
            var response = await api.DeleteContent("v2/pet/dddd");
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(404));
        }
    }
}