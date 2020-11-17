using FluentAssertions;
using HBSIS.Padawan.WebApi.TesteIntegracao.Fixtures;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HBSIS.Padawan.WebApi.TesteIntegracao.Scenarios
{
    public class ValuesTest
    {
        private readonly TestContext _testContext;
        
        public ValuesTest()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Values_GETOK()
        {
            var response = await _testContext.Client.GetAsync("");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        } 

    }
}
