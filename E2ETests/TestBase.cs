using System.Net.Http;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests
{
    public class TestBase : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        protected HttpClient appWebHost;

        protected TestBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateHttpClient()
        {
            appWebHost = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(configureServices =>
                {
                    configureServices.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("memory");
                    });
                });
            }).CreateClient();
            return appWebHost;

            //return _factory.CreateClient();
        }
    }
}