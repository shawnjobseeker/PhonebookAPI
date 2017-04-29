using PhonebookAPI;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

namespace PhonebookAPITests
{
    // based on https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing#integration-testing
    class FunctionalTest : IClassFixture<FunctionalFixture<Startup>>
    {
        FunctionalFixture<Startup> fix;
        public FunctionalTest(FunctionalFixture<Startup> fixture)
        {
            fix = fixture;
        }
        [Fact]
        public async Task TestGetAsync()
        {
            var response = await fix.Client.GetAsync("api/default/");
            Assert.Equal(HttpStatusCode.Found, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.True(content.Contains("Shawn"));
            response = await fix.Client.GetAsync("api/default/2");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
