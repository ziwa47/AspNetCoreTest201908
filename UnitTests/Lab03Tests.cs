using AspNetCoreTest201908.Api.Lab03_IHostEnvironment;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests
{
    public class Lab03Tests
    {
        [Fact]
        public void Host1()
        {
            IHostingEnvironment environment = new HostingEnvironment
            {
                EnvironmentName = "Production"
            };
            var lab03Controller = new Lab03Controller(environment);

            var result = lab03Controller.Index1() as OkObjectResult;

            result.Value.As<EnvResult>().Env.Should().Be("Dev");
        }        
        
        [Fact]
        public void Host2()
        {
            IHostingEnvironment environment = new HostingEnvironment
            {
                EnvironmentName = "Dev"
            };
            var lab03Controller = new Lab03Controller(environment);

            var result = lab03Controller.Index1() as OkObjectResult;

            result.Value.As<EnvResult>().Env.Should().Be("Prod");
        }        
    }
}