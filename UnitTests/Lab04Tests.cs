using AspNetCoreTest201908.Api.Lab04_HttpContext;
using AspNetCoreTest201908.Model;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        [Fact]
        public void Test3()
        {
            var testSession = new TestSession();

            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext = new DefaultHttpContext()
            {
                Session = testSession,
            };
            var lab04Controller = new Lab04Controller(httpContextAccessor);
            var result = lab04Controller.Index3() as OkObjectResult;
            result.Value.As<AuthUser>().User.Should().Be("ziwa");
        }

        [Fact]
        public void Test4()
        {
            var httpContextAccessor = new HttpContextAccessor();
            httpContextAccessor.HttpContext = new DefaultHttpContext()
            {
                Request = { Cookies = new RequestCookieCollection(
                    new Dictionary<string,string>
                    {
                        {"user","ziwa"},
                    })
                }
            };
            var lab04Controller = new Lab04Controller(httpContextAccessor);
            var result = lab04Controller.Index4() as OkObjectResult;
            result.Value.As<AuthUser>().User.Should().Be("ziwa");
        }
    }

    public class TestSession : ISession
    {
        public Task LoadAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            value = Encoding.UTF8.GetBytes("ziwa");
            return true;
        }

        public void Set(string key, byte[] value)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool IsAvailable { get; }
        public string Id { get; }
        public IEnumerable<string> Keys { get; }
    }
}