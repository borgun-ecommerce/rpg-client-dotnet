using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using BorgunPayment.Model;
using BorgunPayment.UnitTests.Mock;

namespace BorgunPayment.UnitTests
{
    [TestClass]
    public class TestTokenSingle
    {
        [TestMethod]
        public void TestCreate()
        {
            TokenSingleRequest req = new TokenSingleRequest()
            {
                PAN = "4242424242424242",
                ExpMonth = "01",
                ExpYear = "20",
                TokenLifetime = 20
            };

            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            TokenSingleResponse response = client.TokenSingle.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
            Assert.IsFalse(String.IsNullOrEmpty(response.Uri));
            Assert.IsNotNull(response.Token);
            Assert.AreEqual("TestTokenSingle", response.Token.Token);
        }

        [TestMethod]
        public void TestGet()
        {
            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            TokenSingleResponse response = client.TokenSingle.GetAsync("testtoken").Result;
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Token);
            Assert.AreEqual("TestTokenSingle", response.Token.Token);
        }

        [TestMethod]
        public void TestDelete()
        {
            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            TokenSingleResponse response = client.TokenSingle.DeleteAsync("testtoken").Result;
            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNull(response.Token);
        }
    }
}
