using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.IO;
using BorgunPayment.Model;
using System.Net;

namespace BorgunPayment.IntegrationTests
{
    [TestClass]
    public class TestTokenSingle
    {
        private string uri;
        private string testPan;
        private string testExpMonth;
        private string testExpYear;
        private Configuration config;

        public TestTokenSingle()
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
        public void TestCreateToken()
        {
            TokenSingleRequest req = new TokenSingleRequest()
            {
                PAN = this.testPan,
                ExpMonth = this.testExpMonth,
                ExpYear = this.testExpYear,
                TokenLifetime = 20
            };

            RPGClient client = new RPGClient(this.config.AppSettings.Settings["PrivateToken"].Value, this.uri);
            TokenSingleResponse response = client.TokenSingle.CreateAsync(req).Result;

            Assert.AreEqual((int)HttpStatusCode.Created, response.StatusCode);
            Assert.IsFalse(String.IsNullOrEmpty(response.Uri));
            Assert.IsNotNull(response.Token);
            Assert.IsTrue(!String.IsNullOrEmpty(response.Token.Token));
            Assert.AreEqual(this.uri + "api/token/single/" + response.Token.Token, response.Uri);

            req.PAN = "1234567890123456";
            response = client.TokenSingle.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsNull(response.Token);
            Assert.IsFalse(String.IsNullOrEmpty(response.Message));
            Assert.AreEqual("{\"Message\":\"PAN: InvalidFormat\"}", response.Message);

            req.PAN = null;
            req.ExpMonth = null;
            req.ExpYear = null;
            req.TokenLifetime = 0;

            response = client.TokenSingle.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsNull(response.Token);
            Assert.IsFalse(String.IsNullOrEmpty(response.Message));
            Assert.AreEqual("{\"Message\":\"PAN: InvalidFormat; ExpMonth: Invalid expiration date; TokenLifetime: 'Token Lifetime' must be greater than '0'.\"}", response.Message);

            client = new RPGClient("InvalidKey", this.uri);
            response = client.TokenSingle.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public void TestGetToken()
        {
            TokenSingleRequest req = new TokenSingleRequest()
            {
                PAN = this.testPan,
                ExpMonth = this.testExpMonth,
                ExpYear = this.testExpYear,
                TokenLifetime = 20
            };

            RPGClient client = new RPGClient(this.config.AppSettings.Settings["PrivateToken"].Value, this.uri);
            TokenSingleResponse response = client.TokenSingle.CreateAsync(req).Result;
            Assert.IsNotNull(response.Token);
            Assert.IsFalse(String.IsNullOrEmpty(response.Token.Token));

            TokenSingleResponse tokenResponse = client.TokenSingle.GetAsync(response.Token.Token).Result;
            Assert.AreEqual((int)HttpStatusCode.OK, tokenResponse.StatusCode);
            Assert.IsNotNull(tokenResponse.Token);
            Assert.AreEqual(response.Token.Token, tokenResponse.Token.Token);

            tokenResponse = client.TokenSingle.GetAsync("InvalidToken").Result;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, tokenResponse.StatusCode);

            tokenResponse = client.TokenSingle.GetAsync("ts_invalidtoken").Result;
            Assert.AreEqual((int)HttpStatusCode.NotFound, tokenResponse.StatusCode);

            client = new RPGClient("InvalidKey", this.uri);
            tokenResponse = client.TokenSingle.CreateAsync(req).Result;
            Assert.AreEqual((int)HttpStatusCode.Unauthorized, tokenResponse.StatusCode);
        }

        [TestMethod]
        public void TestDisableToken()
        {
            TokenSingleRequest req = new TokenSingleRequest()
            {
                PAN = this.testPan,
                ExpMonth = this.testExpMonth,
                ExpYear = this.testExpYear,
                TokenLifetime = 20
            };

            RPGClient client = new RPGClient(this.config.AppSettings.Settings["PrivateToken"].Value, this.uri);
            TokenSingleResponse response = client.TokenSingle.CreateAsync(req).Result;
            Assert.IsNotNull(response.Token);
            Assert.IsFalse(String.IsNullOrEmpty(response.Token.Token));

            TokenSingleResponse tokenResponse = client.TokenSingle.DisableAsync(response.Token.Token).Result;
            Assert.AreEqual((int)HttpStatusCode.NoContent, tokenResponse.StatusCode);
            tokenResponse = client.TokenSingle.GetAsync(response.Token.Token).Result;
            Assert.AreEqual(response.Token.Token, tokenResponse.Token.Token);
            Assert.IsFalse(tokenResponse.Token.Enabled);

            tokenResponse = client.TokenSingle.DisableAsync("InvalidToken").Result;
            Assert.AreEqual((int)HttpStatusCode.BadRequest, tokenResponse.StatusCode);

            tokenResponse = client.TokenSingle.DisableAsync("ts_invalidtoken").Result;
            Assert.AreEqual((int)HttpStatusCode.NotFound, tokenResponse.StatusCode);
        }
    }
}
