using BorgunPayment.Model;
using BorgunPayment.UnitTests.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BorgunPayment.UnitTests
{
    [TestClass]
    class TestTokenMulti
    {
        [TestMethod]
        public void TestCreate()
        {
            TokenMultiRequest req = new TokenMultiRequest()
            {
                PAN = "4242424242424242",
                ExpMonth = "01",
                ExpYear = "20"
            };

            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            TokenMultiResponse response = client.TokenMulti.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
            Assert.IsFalse(String.IsNullOrEmpty(response.Uri));
            Assert.IsNotNull(response.Token);
            Assert.AreEqual("TestTokenMulti", response.Token.Token);
        }

        [TestMethod]
        public void TestGet()
        {
            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            TokenMultiResponse response = client.TokenMulti.GetAsync("testtoken").Result;
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Token);
            Assert.AreEqual("TestTokenMulti", response.Token.Token);
        }

        [TestMethod]
        public void TestDelete()
        {
            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            TokenMultiResponse response = client.TokenMulti.DeleteAsync("testtoken").Result;
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNull(response.Token);
        }
    }
}
