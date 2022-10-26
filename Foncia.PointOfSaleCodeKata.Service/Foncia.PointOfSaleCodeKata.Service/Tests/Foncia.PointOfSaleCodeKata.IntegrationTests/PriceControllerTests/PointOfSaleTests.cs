using System.Threading.Tasks;
using Foncia.PointOfSaleCodeKata.IntegrationTests.BaseTests;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Foncia.PointOfSaleCodeKata.IntegrationTests.PriceControllerTests
{
    public class PointOfSaleTests : BaseWebApiTests
    {
        private PointOfSaleTerminal _terminal;
        
        public PointOfSaleTests(WebApplicationFactory<Startup> fixture) : base(fixture)
        {
            _terminal = new PointOfSaleTerminal(fixture);
        }

        [Fact]
        public async Task ShouldProcessFirstCaseSuccessfully()
        {
            // arrange
            _terminal.Clear();
            _terminal.Scan("A");
            _terminal.Scan("B");
            _terminal.Scan("C");
            _terminal.Scan("D");
            _terminal.Scan("A");
            _terminal.Scan("B");
            _terminal.Scan("A");

            // act
            var res = await _terminal.CalculateTotal();

            // assert
            Assert.Equal(13.25M, res);
        }

        [Fact]
        public async Task ShouldProcessSecondCaseSuccessfully()
        {
            // arrange
            _terminal.Clear();
            _terminal.Scan("C");
            _terminal.Scan("C");
            _terminal.Scan("C");
            _terminal.Scan("C");
            _terminal.Scan("C");
            _terminal.Scan("C");
            _terminal.Scan("C");

            // act
            var res = await _terminal.CalculateTotal();

            // assert
            Assert.Equal(6M, res);
        }

        [Fact]
        public async Task ShouldProcessThirdCaseSuccessfully()
        {
            // arrange
            _terminal.Clear();
            _terminal.Scan("A");
            _terminal.Scan("B");
            _terminal.Scan("C");
            _terminal.Scan("D");

            // act
            var res = await _terminal.CalculateTotal();

            // assert
            Assert.Equal(7.25M, res);
        }
    }
}
