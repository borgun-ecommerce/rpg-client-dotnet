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
    public class TestPayment
    {
        [TestMethod]
        public void TestCreatePayment()
        {
            PaymentRequest req = new PaymentRequest()
            {
                PaymentMethod = new PaymentRequestMethod()
                {
                    PaymentType = PaymentTypes.Card,
                    PAN = "4242424242424242",
                    ExpMonth = "01",
                    ExpYear = "20",
                    CVC2 = "000"
                },
                Amount = 100,
                Currency = "352",
                OrderId = "TEST00000001",
                TransactionDate = DateTime.Now,
                TransactionType = TransactionTypes.Sale
            };

            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            PaymentTransactionResponse response = client.Payment.CreateAsync(req).Result;

            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
            Assert.IsFalse(String.IsNullOrEmpty(response.Uri));
            Assert.IsNotNull(response.Transaction);
            Assert.AreEqual("TestTransaction", response.Transaction.TransactionId);
        }

        [TestMethod]
        public void TestGetTransaction()
        {
            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            PaymentTransactionResponse response = client.Payment.GetTransactionAsync("TestTransaction").Result;

            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Transaction);
            Assert.AreEqual("TestTransaction", response.Transaction.TransactionId);
        }

        [TestMethod]
        public void TestCancelTransaction()
        {
            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            PaymentCancelResponse response = client.Payment.CancelAsync("TestTransaction").Result;

            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual("TestTransaction", response.Result.TransactionId);
        }

        [TestMethod]
        public void TestRefundTransaction()
        {
            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            PaymentRefundResponse response = client.Payment.RefundAsync("TestTransaction").Result;

            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual("TestTransaction", response.Result.TransactionId);
        }

        [TestMethod]
        public void TestCaptureTransaction()
        {
            RPGClient client = new RPGClient("myKey", "http://www.borgun.is/", new HttpMessageHandlerMock());
            PaymentCaptureResponse response = client.Payment.CaptureAsync("TestTransaction").Result;

            Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual("TestTransaction", response.Result.TransactionId);
        }
    }
}
