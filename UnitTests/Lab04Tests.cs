using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using AspNetCoreTest201908.Api.Lab02_IConfiguration;
using AspNetCoreTest201908.Api.Lab04_HttpContext;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests
{
    public class Lab04Tests
    {
        [Fact]
        public void Test11()
        {
            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(new GenericIdentity("User")),
            };
            var lab04Controller = new Lab04Controller(httpContextAccessor);
            var result = lab04Controller.Index1() as OkObjectResult;
            result.Value.As<AuthResult>().IsAuth.Should().BeTrue();
        }
        [Fact]
        public void Test12()
        {
            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext = new DefaultHttpContext()
            {
            };
            var lab04Controller = new Lab04Controller(httpContextAccessor);
            var result = lab04Controller.Index1() as OkObjectResult;
            result.Value.As<AuthResult>().IsAuth.Should().BeFalse();
        }
        [Fact]
        public void Test21()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,"abc@a.com"),
                new Claim("MyType","myType"),
            };

            var httpContextAccessor = new HttpContextAccessor();
            ClaimsIdentity identities = new ClaimsIdentity(claims);
            httpContextAccessor.HttpContext = new DefaultHttpContext()
            {
                User = new ClaimsPrincipal(identities),
            };
            var lab04Controller = new Lab04Controller(httpContextAccessor);
            var result = lab04Controller.Index2() as OkObjectResult;
            result.Value.As<AuthClaim>().Email.Should().Be("abc@a.com");
            result.Value.As<AuthClaim>().MyType.Should().Be("myType");

        }

    }
}