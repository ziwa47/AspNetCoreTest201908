using System.Collections.Generic;
using AspNetCoreTest201908.Api.Lab02_IConfiguration;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Xunit;

namespace UnitTests
{
    public class Lab02Tests
    {
        [Fact]
        public void Config01()
        {
            var keyValuePairs = new Dictionary<string, string>
            {
                { "Server:Host", "127.0.0.1" }
            };
            IConfiguration configuration = new ConfigurationBuilder()
                                           .AddInMemoryCollection(keyValuePairs)
                                           .Build();
            var lab021Controller = new Lab021Controller(configuration);

            var result = lab021Controller.Index1() as OkObjectResult;

            result.Value.As<ServerResult>().Host.Should().Be("127.0.0.1");
        }
        
        [Fact]
        public void Config02()
        {
            var keyValuePairs = new Dictionary<string, string>
            {
                { "Server:Host", "127.0.0.1" }
            };
            IConfiguration configuration = new ConfigurationBuilder()
                                           .AddInMemoryCollection(keyValuePairs)
                                           .Build();
            var lab021Controller = new Lab021Controller(configuration);

            var result = lab021Controller.Index2() as OkObjectResult;

            result.Value.As<ServerResult>().Host.Should().Be("127.0.0.1");
        }
       
        [Fact]
        public void Config03()
        {
            var optionsWrapper = new OptionsWrapper<ServerHost>(new ServerHost
            {
                Host = "127.0.0.1"
            });
            var lab022Controller = new Lab022Controller(optionsWrapper);

            var result = lab022Controller.Index1() as OkObjectResult;
            result.Value.As<ServerResult>().Host.Should().Be("127.0.0.1");
        }
    }
}