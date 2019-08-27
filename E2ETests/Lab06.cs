using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Api.Lab00;
using AspNetCoreTest201908.Api.Lab06_Service;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests
{
    public class Lab06 : TestBase
    {
        public Lab06(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test()
        {
            var httpClient = CreateHttpClient(service =>
            {
                service.AddScoped<IHttpService, FakeHttpService>();
            });
            var httpResponseMessage = await httpClient.GetAsync("api/Lab06/Index1");

            var result = httpResponseMessage.Content.ReadAsAsync<AuthResult>();
            result.Result.IsAuth.Should().BeTrue();

        }
        [Fact]
        public async Task Test01()
        {
            CreateHttpClient();

            Operator<IHttpService>(async httpService =>
            {
                var result = await httpService.IsAuthAsync();
                result.Should().BeTrue();
            });

        }
        //[Fact]
        //public async Task TestUnitTest()
        //{
        //    var buildServiceProvider = new ServiceCollection()
        //        .AddHttpClient()
        //        .AddScoped<IHttpService, HttpService>()
        //        .BuildServiceProvider();
        //    using (var serviceScope = buildServiceProvider.CreateScope())
        //    {
        //        var httpService = serviceScope.ServiceProvider.GetRequiredService<IHttpService>();
        //        var result = await httpService.IsAuthAsync();
        //        result.Should().BeTrue();
        //    }

            
        //}
        [Fact]
        public async Task TestFail()
        {
            var httpClient = CreateHttpClient(service =>
            {
                service.AddScoped<IHttpService, FakeFailHttpService>();
            });
            var httpResponseMessage = await httpClient.GetAsync("api/Lab06/Index1");

            var result = httpResponseMessage.Content.ReadAsAsync<AuthResult>();
            result.Result.IsAuth.Should().BeFalse();
        }

        public class FakeHttpService : IHttpService
        {
            public Task<bool> IsAuthAsync()
            {
                return Task.FromResult(true);
            }
        }
        public class FakeFailHttpService : IHttpService
        {
            public Task<bool> IsAuthAsync()
            {
                return Task.FromResult(false);
            }
        }
    }
}