using Newtonsoft.Json;
using PetStoreAPI;
using PetStoreAPI.Models;
using RestSharp;
using RichardSzalay.MockHttp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MockApiTests
{
    [TestFixture]
    public class MockTests
    {
        private ApiCalls api = new();
        [Test]
        public async Task PetsGet200StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.OK);
            Assert.That(response, Is.EqualTo(200));
        }

        [Test]
        public async Task PetsGet308StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.PermanentRedirect);
            Assert.That(response, Is.EqualTo(308));
        }

        [Test]
        public async Task PetsGet302StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.Redirect);
            Assert.That(response, Is.EqualTo(302));
        }

        [Test]
        public async Task PetsGet304StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.NotModified);
            Assert.That(response, Is.EqualTo(304));
        }

        [Test]
        public async Task PetsGet400StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.BadRequest);
            Assert.That(response, Is.EqualTo(400));
        }

        [Test]
        public async Task PetsGet401StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.Unauthorized);
            Assert.That(response, Is.EqualTo(401));
        }

        [Test]
        public async Task PetsGet403StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.Forbidden);
            Assert.That(response, Is.EqualTo(403));
        }

        [Test]
        public async Task PetsGet404StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("petStore", HttpStatusCode.NotFound);
            Assert.That(response, Is.EqualTo(404));
        }

        [Test]
        public async Task PetsGet500StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.InternalServerError);
            Assert.That(response, Is.EqualTo(500));
        }

        [Test]
        public async Task PetsGet501StatusCodeMockApi()
        {
            var response = await api.GetStatusCodeMockApi("available", HttpStatusCode.NotImplemented);
            Assert.That(response, Is.EqualTo(501));
        }

        [Test]
        public async Task PetsPost201StatusCodeMockApi()
        {
            var response = await api.PostStatusCodeMockApi("available", HttpStatusCode.Created);
            Assert.That(response, Is.EqualTo(201));
        }

        [Test]
        public async Task PetsPut200StatusCodeMockApi()
        {
            var response = await api.PutStatusCodeMockApi("available", HttpStatusCode.OK);
            Assert.That(response, Is.EqualTo(200));
        }

        [Test]
        public async Task PetsDelete200StatusCodeMockApi()
        {
            var response = await api.DeleteStatusCodeMockApi("available", HttpStatusCode.OK);
            Assert.That(response, Is.EqualTo(200));
        }
    }
}
