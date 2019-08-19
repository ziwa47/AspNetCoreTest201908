using AspNetCoreTest201908.Api.Lab03_IHostEnvironment;
using AspNetCoreTest201908.Model;

using FluentAssertions;

using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace UnitTests
{
    public class Lab03Tests
    {
        [Fact]
        public void Test()
        {
            var sut = new Lab03Controller(new HostingEnvironment(){
                EnvironmentName = "Development"
            });
            var envResult = sut.Index1() as OkObjectResult;
            envResult.Value.As<EnvResult>().Env.Should().Be("Prod");
        }

        [Fact]
        public void Test2()
        {
            var lab01Controller = new Lab03Controller(new HostingEnvironment() {
                EnvironmentName = "Production"
            });
            var envResult = lab01Controller.Index1() as OkObjectResult;
            envResult.Value.As<EnvResult>().Env.Should().Be("Dev");
        }
    }
}