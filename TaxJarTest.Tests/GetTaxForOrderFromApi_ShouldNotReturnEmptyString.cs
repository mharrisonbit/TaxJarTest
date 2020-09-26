using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TaxJarTest.RestCalls;

namespace TaxJarTest.Tests
{
    [TestFixture]
    public class GetTaxForOrderFromApi_ShouldNotReturnEmptyStringShould
    {
        private JObject passingTaxRateObject;
        private JObject failingTaxRateObject;

        [SetUp]
        public void Setup()
        {
            Xamarin.Forms.Mocks.MockForms.Init();

            passingTaxRateObject = new JObject
            {
                { "to_country", "US" },
                { "to_zip", "07446" },
                { "to_state", "NJ" },
                { "shipping", 1.50 },
                { "amount", 16.50 },
                { "from_zip", "07001"},
                { "from_state", "NJ"},
            };

            failingTaxRateObject = new JObject
            {
                { "to_country", "US" },
                { "to_zip", ")7446" },
                { "to_state", "NJ" },
                { "shipping", 1.50 },
                { "amount", 16.50 },
                { "from_zip", "07001"},
                { "from_state", "NJ"},
            };
        }

        [Test]
        public async Task PassingTest()
        {
            var classSetup = new GetDataCalls();
            var taxRateStringReturned = await classSetup.GetTaxForOrderFromApi(passingTaxRateObject);
            taxRateStringReturned.Should().NotBe("");
        }

        [Test]
        public async Task FailingTest()
        {
            var classSetup = new GetDataCalls();
            var taxRateStringReturned = await classSetup.GetTaxForOrderFromApi(failingTaxRateObject);
            taxRateStringReturned.Should().Be("");
        }
    }
}
