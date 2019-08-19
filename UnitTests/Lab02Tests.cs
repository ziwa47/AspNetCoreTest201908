using System.Collections.Generic;
using System.Configuration;
using AspNetCoreTest201908.Api.Lab01_ILogger;
using AspNetCoreTest201908.Api.Lab02_IConfiguration;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace UnitTests
{
    public class Lab02Tests
    {
        private string hostIp = "192.168.1.101";
        private string configureKey = "Server:Host";

        [Fact]
        public void Test()
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {configureKey,hostIp},
                })
                .Build();
            var lab01Controller = new Lab021Controller(configuration);

            var result = lab01Controller.Index1() as OkObjectResult;
            result.Value.As<ServerResult>().Host.Should().BeEquivalentTo(hostIp);
        }
        [Fact]
        public void Test2()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {configureKey,hostIp},
                })
                .Build();
            var lab01Controller = new Lab021Controller(configuration);

            var result = lab01Controller.Index2() as OkObjectResult;
            result.Value.As<ServerResult>().Host.Should().BeEquivalentTo(hostIp);
        }
        [Fact]
        public void Test3()
        {
            var lab01Controller = new Lab022Controller(
                new OptionsWrapper<ServerHost>(
                    new ServerHost()
                    {
                        Host = hostIp
                    }));

            var result = lab01Controller.Index1() as OkObjectResult;
            result.Value.As<ServerResult>().Host.Should().BeEquivalentTo(hostIp);
        }
    }


}