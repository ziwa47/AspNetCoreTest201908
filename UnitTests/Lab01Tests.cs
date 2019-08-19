using System;
using AspNetCoreTest201908.Api.Lab00;
using AspNetCoreTest201908.Api.Lab01_ILogger;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace UnitTests
{
    public class Lab01Tests
    {
        [Fact]
        public void Test()
        {
            var lab01Controller = new Lab01Controller(new NullLogger<Lab01Controller>());

            var result = lab01Controller.Index1() as OkObjectResult;
            result.Value.As<AuthResult>().IsAuth.Should().BeTrue();
        }
    }
}