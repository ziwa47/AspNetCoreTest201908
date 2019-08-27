using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace E2ETests
{
    public class Lab022 : TestBase
    {
        public Lab022(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test01()
        {
            var httpClient = CreateHttpClient();  
            var httpResponseMessage =  await httpClient.GetAsync("api/Lab022/Index1");
            var result = httpResponseMessage.Content.ReadAsAsync<ServerResult>();
            result.Result.Host.Should().Be("10.10.0.1");
        }
    }
}
