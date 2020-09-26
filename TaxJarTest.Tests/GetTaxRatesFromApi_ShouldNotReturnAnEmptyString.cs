using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TaxJarTest.Interfaces;
using TaxJarTest.RestCalls;

namespace TaxJarTest.Tests
{
    [TestFixture]
    public class GetTaxRatesFromApi_ShouldNotReturnAnEmptyStringShould
    {
        private JObject passingTaxRateObject;
        private JObject failingTaxRateObject;

        public Mock<IGetDataCalls> GetDataMock { get; private set; }

        [SetUp]
        public void Setup()
        {
            passingTaxRateObject = new JObject
            {
                { "country", "US" },
                { "zipcode", "41224" },
                { "city", "Inez" }
            };

            failingTaxRateObject = new JObject
            {
                { "country", "US" },
                { "zipcode", "" },
                { "city", "Inez" }
            };
        }

        [Test]
        public async Task PassingTest()
        {
            var classSetup = new GetDataCalls();
            var taxRateStringReturned = await classSetup.GetTaxRatesFromApi(passingTaxRateObject);
            taxRateStringReturned.Should().NotBe("");
        }

        [Test]
        public async Task FailingTest()
        {
            var classSetup = new GetDataCalls();
            var taxRateStringReturned = await classSetup.GetTaxRatesFromApi(failingTaxRateObject);
            taxRateStringReturned.Should().Be("");
        }
    }
}
