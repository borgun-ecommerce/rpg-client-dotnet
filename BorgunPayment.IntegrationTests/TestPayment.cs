using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BorgunPayment.Model;
using System.Net;

namespace BorgunPayment.IntegrationTests
{
    [TestClass]
    public class TestPayment
    {
        private string uri;
        private string testPan;
        private string testExpMonth;
        private string testExpYear;
        private Configuration config;

        public TestPayment()
        {
            // Custom configuration file support.
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ExternalConfigPath"]) && File.Exists(ConfigurationManager.AppSettings["ExternalConfigPath"]))
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                configMap.ExeConfigFilename = ConfigurationManager.AppSettings["ExternalConfigPath"];
                this.config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            }
            else
            {
                this.config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }

            this.testPan = this.config.AppSettings.Settings["TestCardPan"].Value;
            this.testExpMonth = this.config.AppSettings.Settings["TestCardExpMonth"].Value;
            this.testExpYear = this.config.AppSettings.Settings["TestCardExpYear"].Value;
            this.uri = this.config.AppSettings.Settings["URI"].Value;
        }

        [TestMethod]
        public void TestCreatePayment()
        {
            PaymentRequest req = new PaymentRequest()
            {
                TransactionType = TransactionTypes.PreAuthorization,
                PaymentMethod = new PaymentRequestMethod()
                {
                    PaymentType = PaymentTypes.Card,
                    PAN = this.testPan,
                    ExpMonth = this.testExpMonth,
                    ExpYear = this.testExpYear
                },
                Amount = 100,
                Currency = "352",
                OrderId = "IntegrTest01",
                TransactionDate = DateTime.Now
            };

            RPGClient client = new RPGClient(this.config.AppSettings.Settings["PrivateToken"].Value, this.uri);
            PaymentTransactionResponse response = client.Payment.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(this.uri + "api/payment/" + response.Transaction.TransactionId, response.Uri);
            Assert.AreEqual("000", response.Transaction.ActionCode);
            Assert.AreEqual(TransactionStatus.Uncaptured, response.Transaction.TransactionStatus);
            Assert.AreEqual(TransactionTypes.PreAuthorization, response.Transaction.TransactionType);

            req = new PaymentRequest();
            response = client.Payment.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(String.IsNullOrEmpty(response.Message));

            client = new RPGClient("InvalidKey", this.uri);
            response = client.Payment.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
