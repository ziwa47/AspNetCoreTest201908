using System;
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
        protected WebApplicationFactory<Startup> AppWebHost;

        protected TestBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateHttpClient()
        {
            AppWebHost = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(configureServices =>
                {
                    configureServices.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("memory");
                    });
                });
            });
            return AppWebHost.CreateClient();

            //return _factory.CreateClient();
        }

        protected void DbOperator(Action<AppDbContext> action)
        {
            using (var serviceScope = AppWebHost.Server.Host.Services.CreateScope())
            {
                var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                action.Invoke(appDbContext);
            }
        }
    }
}