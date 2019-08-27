using AspNetCoreTest201908.Api.Lab00;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests
{
    public class Lab00Tests
    {
        [Fact]
        public void Test()
        {
            var lab00Controller = new Lab00Controller();
            var result = lab00Controller.Index() as OkObjectResult;
            result.Value.As<AuthResult>().IsAuth.Should().BeTrue();
        }
        [Fact]
        public void Test1()
        {
            var lab00Controller = new Lab00Controller();
            var result = lab00Controller.Index2();
            result.Value.As<AuthResult>().IsAuth.Should().BeTrue();
        }
    }
}