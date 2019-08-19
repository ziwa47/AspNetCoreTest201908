using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AspNetCoreTest201908.Api.Lab04_HttpContext;
using AspNetCoreTest201908.Api.Lab05_DbContext;
using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests
{
    public class Lab05Tests
    {
        [Fact]
        public async Task Test05()
        {
            var appDbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            var expectations = new List<Profile>
            {
                new Profile(){Id = Guid.NewGuid(),Name = "ziwa"},
                new Profile(){Id = Guid.NewGuid(),Name = "ziwa2"},
            };
            appDbContext.Profile.AddRange(expectations);
            appDbContext.SaveChanges();

            var lab04Controller = new Lab05Controller(appDbContext);
            var result = await lab04Controller.Index1() as OkObjectResult;

            result.Value.As<List<Profile>>()
                .Should().BeEquivalentTo(expectations);
        }
        [Fact]
        public async Task Test05_sqlite()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                //.UseSqlite($"DataSource=:{Guid.NewGuid().ToString()}:")
                //.UseSqlite($@"DataSource=C:\TESTDb\DXX.db")
                .UseSqlite($@"DataSource=:memory:")
                .Options;

            var appDbContext = new AppDbContext(dbContextOptions);

            appDbContext.Database.OpenConnection();
            appDbContext.Database.EnsureCreated();

            var expectations = new List<Profile>
            {
                new Profile(){Id = Guid.NewGuid(),Name = "ziwa"},
                new Profile(){Id = Guid.NewGuid(),Name = "ziwa2"},
            };
            appDbContext.Profile.AddRange(expectations);
            appDbContext.SaveChanges();

            var lab04Controller = new Lab05Controller(appDbContext);
            var result = await lab04Controller.Index1() as OkObjectResult;

            result.Value.As<List<Profile>>()
                .Should().BeEquivalentTo(expectations);

            appDbContext.Database.CloseConnection();
            appDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public async Task Test052()
        {
            var appDbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            var lab04Controller = new Lab05Controller(appDbContext);
            var result = await lab04Controller.Index2(new ProfileDto()
            {
                Name = "ziwa",
            }) as OkObjectResult;

            result.Value.As<Profile>().Id.Should().NotBeEmpty();
            result.Value.As<Profile>().Name.Should().Be("ziwa");
        }

    }
}