using System;
using System.Net.Http;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
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

        protected HttpClient CreateHttpClient(Action<IServiceCollection> servicesConfiguration = null)
        {
            AppWebHost = _factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");

                if (servicesConfiguration != null)
                {
                    builder.ConfigureTestServices(servicesConfiguration);
                    // Use Autofac
                    //builder.ConfigureTestContainer<ContainerBuilder>();
                }

                builder.ConfigureServices(configureServices =>
                {
                    configureServices.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseSqlite("DataSource=name");
                        //options.UseInMemoryDatabase("memory");
                    });

                    var serviceProvider = configureServices.BuildServiceProvider();
                    using (var serviceScope = serviceProvider.CreateScope())
                    {
                        var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                        appDbContext.Database.EnsureDeleted();
                        appDbContext.Database.EnsureCreated();

                        appDbContext.Database.ExecuteSqlCommand("create view VProfile as select Name from Profile");

                    }
                });
            });
            return AppWebHost.CreateClient();

            //return _factory.CreateClient();
        }

        protected void DbOperator(Action<AppDbContext> action)
        {
            Operator(action);
        }
        protected void Operator<T>(Action<T> action)
        {
            using (var serviceScope = AppWebHost.Server.Host.Services.CreateScope())
            {
                var t = serviceScope.ServiceProvider.GetRequiredService<T>();
                action.Invoke(t);
            }
        }
    }
}