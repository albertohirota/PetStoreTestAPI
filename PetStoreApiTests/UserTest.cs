using Newtonsoft.Json;
using PetStoreAPI;
using PetStoreAPI.Models;

namespace UserTests
{

    [TestFixture]
    public class PositveTestCases
    {
        public ApiCalls api = new();
        public ApiHelper apiHelper = new();

        [TearDown]
        public void BaseTearDown()
        {
            _ = api.DeleteContent("v2/user/99", "ah", "1234");
        }

        [SetUp]
        public async Task SetupUser()
        {
            var obj = apiHelper.GetUserJsonObject(99, "ah", "Alberto", "Hirota", "1234");
            var jsonResult = JsonConvert.SerializeObject(obj);
            var response = await api.PostContent("v2/user/", jsonResult);
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }

        [Test]
        public async Task UserLogIn()
        {
            var response = await api.GetApiContent("v2/user/login/", "ah", "1234");
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
            _ = await api.GetApiContent("v2/user/logout/");
        }

        [Test]
        public async Task UpdateUser()
        {
            string userName = "ah";
            var obj = apiHelper.GetUserJsonObject(99, userName , "Jonny", "Hirota", "1234");
            var jsonResult = JsonConvert.SerializeObject(obj);
            var response = await api.PutContent("v2/user/"+ userName, jsonResult, "ah", "1234");
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }

        [Test]
        public async Task UserLogOut()
        {
            var response = await api.GetApiContent("v2/user/logout/", "ah", "1234"); ;
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteUser()
        {
            string userName = "ah";
            var response = await api.DeleteContent("v2/user/" + userName, "ah", "1234"); ;
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }

        [Test]
        public async Task GetUser()
        {
            string userName = "ah";
            var response = await api.DeleteContent("v2/user/" + userName, "ah", "1234"); ;
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }

        [Test]
        public async Task PostWithList()
        {
            var obj = apiHelper.GetUserJsonObject(99, "ah", "Alberto", "Hirota", "1234");
            List<Users> userList = new()
            {
                obj
            };
            var jsonResult = JsonConvert.SerializeObject(userList);
            var response = await api.PostContent("v2/user/createWithList/", jsonResult);
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }

        [Test]
        public async Task PostWithArray()
        {
            var obj = apiHelper.GetUserJsonObject(99, "ah", "Alberto", "Hirota", "1234");
            Users[] userArray = new Users[] { obj};
            var jsonResult = JsonConvert.SerializeObject(userArray);
            var response = await api.PostContent("v2/user/createWithList/", jsonResult);
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }
    }
}
