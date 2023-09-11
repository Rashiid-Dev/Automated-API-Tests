using API;
using API.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private CommonMethods _commonMethods;
        private API.APICalls _apiCalls;

        [SetUp]
        public void Setup()
        {
            _commonMethods = new CommonMethods();
            _apiCalls = new API.APICalls();
        }

        [Test]
        public async Task VerifyClientDetails()
        {
            ClientModel clientDetails = _apiCalls.GetClient("4e41a707-feb1-4b29-80a3-ebc2795b266f");
            Assert.That(clientDetails.Name, Is.EqualTo("JohnSmithTest2"));
            Assert.That(clientDetails.Id, Is.EqualTo("4e41a707-feb1-4b29-80a3-ebc2795b266f"));
            Assert.That(clientDetails.ClientType, Is.EqualTo(ClientTypeEnum.active));
            Assert.That(clientDetails.DateOfBirth, Is.EqualTo(DateTime.ParseExact("1984-09-09", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)));
            Assert.That(clientDetails.Jurisdictions.First, Is.EqualTo("uk"));
        }
        [Test]
        public async Task VerifyClientCreation()
        {
            var client = _apiCalls.CreateClient(_commonMethods.NameRandomizer(), ClientTypeEnum.active, "1984-09-09", "uk");
            var clientID = client.Id;
            Assert.NotNull(clientID, $"Client ID is not null and has been returned. The client ID is {clientID}");
        }

        [Test]
        public async Task VerifyClientCreationUsingModelToJSON()
        {
            var client = _apiCalls.CreateClientUsingModelToJSON(_commonMethods.NameRandomizer(), ClientTypeEnum.active, "1984-09-09", "uk");
            var clientID = client.Id;
            Assert.NotNull(clientID, $"Client ID is not null and has been returned. The client ID is {clientID}");
        }

        [Test]
        public async Task VerifyClientPromotion()
        {
            var client = _apiCalls.CreateClient(_commonMethods.NameRandomizer(), ClientTypeEnum.prospect, "1984-09-09", "uk");
            var clientID = client.Id;
            var promotedClient = _apiCalls.PromoteClient(clientID);
            Assert.That(promotedClient.ClientType, Is.EqualTo(ClientTypeEnum.active));
        }

        [Test]
        public async Task VerifyProductCreation()
        {
            var generatedName = _commonMethods.NameRandomizer();
            var generatedDescription = $"A pair of amazing {_commonMethods.NameRandomizer()}";
            var product = _apiCalls.CreateProduct(generatedName, "uk", 273.21, generatedDescription);
            Assert.That(product.Id, Is.Not.Null);
            Assert.That(product.Name, Is.EqualTo(generatedName));
            Assert.That(product.Description, Is.EqualTo(generatedDescription));
            Assert.That(product.Price, Is.EqualTo(273.21));
            Assert.That(product.Jurisdictions.First, Is.EqualTo("uk"));
            Assert.That(product.DateCreated.Date, Is.EqualTo(DateTime.Today.Date));
        }

        [Test]
        public async Task VerifyProductDetails()
        {
            ProductModel product = _apiCalls.GetProduct("6503a3a4-b9ff-47ba-9e9e-064cd0330c19");
            Assert.That(product.Id, Is.EqualTo("6503a3a4-b9ff-47ba-9e9e-064cd0330c19"));
            Assert.That(product.Name, Is.EqualTo("Office Chairs"));
            Assert.That(product.Price, Is.EqualTo(1645.0));
            Assert.That(product.Description, Is.EqualTo("a pair of comfy office chairs"));
            Assert.That(product.Jurisdictions.First, Is.EqualTo("uk"));
            Assert.That(product.DateCreated.Date.ToString().Split()[0], Is.EqualTo("09/09/2023"));
        }

        [Test]
        public async Task VerifySaleCreation()
        {
            var generatedDescription = $"Sale for {_commonMethods.NameRandomizer()}";
            SalesModel sale = _apiCalls.CreateSale(generatedDescription, 6, "c5d02931-b07e-432e-a2c8-0249feab2e4a", "6503a3a4-b9ff-47ba-9e9e-064cd0330c19");
            Assert.That(sale.Id, Is.Not.Null);
            Assert.That(sale.Id, Is.EqualTo("6503a3a4-b9ff-47ba-9e9e-064cd0330c19"));
            Assert.That(sale.Description, Is.EqualTo(generatedDescription));
            Assert.That(sale.Quantity, Is.EqualTo(6));
            Assert.That(sale.ClientId, Is.EqualTo("c5d02931-b07e-432e-a2c8-0249feab2e4a"));
            Assert.That(sale.ProductId, Is.EqualTo("6503a3a4-b9ff-47ba-9e9e-064cd0330c19"));
        }

        [Test]
        public async Task VerifyGetSale()
        {
            SalesModel sale = _apiCalls.GetSales("c5d02931-b07e-432e-a2c8-0249feab2e4a");
            Assert.That(sale.Id, Is.Not.Null);
            Assert.That(sale.Id, Is.EqualTo("6503a3a4-b9ff-47ba-9e9e-064cd0330c19"));
            Assert.That(sale.Description, Is.EqualTo("Sale for new desks"));
            Assert.That(sale.Quantity, Is.EqualTo(6));
            Assert.That(sale.ClientId, Is.EqualTo("c5d02931-b07e-432e-a2c8-0249feab2e4a"));
            Assert.That(sale.ProductId, Is.EqualTo("6503a3a4-b9ff-47ba-9e9e-064cd0330c19"));
        }
    }
}