using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTest201908;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace E2ETests
{
    public class Lab05 : TestBase
    {
        public Lab05(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test01()
        {
            var httpClient = CreateHttpClient();

            var profile = new List<Profile>()
            {
                new Profile()
                {
                    Id = Guid.NewGuid(),
                    Name = "123"
                }
            };

            DbOperator(appDbContext =>
            {
                appDbContext.Profile.AddRange(profile);
                appDbContext.SaveChanges();
            });

            var httpResponseMessage = await httpClient.GetAsync("api/Lab05/Index1");
            var result = httpResponseMessage.Content.ReadAsAsync<List<Profile>>();
            result.Result.Count.Should().NotBe(0);
            result.Result.Should().BeEquivalentTo(profile);
        }
    }
}