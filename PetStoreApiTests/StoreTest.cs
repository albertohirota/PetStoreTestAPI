using Newtonsoft.Json;
using PetStoreAPI;
using PetStoreAPI.Models;

namespace StoreTests
{
    [TestFixture]
    public class PositiveTestCase
    {
        public ApiCalls api = new();
        public ApiHelper apiHelper = new();

        [TearDown]
        public void BaseTearDown()
        {
            _ = api.DeleteContent("v2/store/999");
            _ = api.DeleteContent("v2/store/998");
            _ = api.DeleteContent("v2/store/997");
        }

        [Test]
        public async Task PostContent()
        {
            var obj = apiHelper.GetStoreJsonObject(999, 10);
            var jsonResult = JsonConvert.SerializeObject(obj);
            var response = await api.PostContent("v2/store/order/", jsonResult);
            var content = response.Content;
            Stores? store = JsonConvert.DeserializeObject<Stores>(content);
            var storeOrder = store?.id;
            Assert.That(storeOrder, Is.EqualTo(999));
        }

        [Test]
        public async Task GetContentById()
        {
            var obj = apiHelper.GetStoreJsonObject(998, 11);
            var jsonResult = JsonConvert.SerializeObject(obj);
            await api.PostContent("v2/store/order/", jsonResult);

            var response = await api.GetApiContent("v2/store/order/998");
            var content = response.Content;
            Stores? store = JsonConvert.DeserializeObject<Stores>(content);
            var storeOrder = store?.id;
            Assert.That(storeOrder, Is.EqualTo(998));
        }

        [Test]
        public async Task DeleteContentById()
        {
            var obj = apiHelper.GetStoreJsonObject(997, 15);
            var jsonResult = JsonConvert.SerializeObject(obj);
            await api.PostContent("v2/store/order/", jsonResult);
            var response = await api.DeleteContent("v2/store/order/997");
            var code = (int)response.StatusCode;
            Assert.That(code, Is.EqualTo(200));
        }
    }
}
