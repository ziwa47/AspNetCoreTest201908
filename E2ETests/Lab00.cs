using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace E2ETests
{
    public class Lab00 : TestBase
    {

        public Lab00(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task TestMethod()
        {
            var httpClient = CreateHttpClient();
            var httpResponseMessage = await httpClient.GetAsync("api/Lab00/Index");
            var result = httpResponseMessage.Content.ReadAsAsync<AuthResult>();
            result.Result.IsAuth.Should().BeTrue();
        }
    }
}