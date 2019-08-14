using System;
using System.Threading.Tasks;
using AspNetCoreTest201908.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTest201908.Api.Lab04_DbContext
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class Lab04Controller : Controller
    {
        private readonly AppDbContext _dbContext;

        public Lab04Controller(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index1()
        {
            return Ok(await _dbContext.Profile.ToListAsync());
        }
        
        [HttpPost]
        public async Task<IActionResult> Index2([FromBody] ProfileDto profileDto)
        {
            _dbContext.Profile.Add(new Profile
            {
                Id = Guid.NewGuid(),
                Name = profileDto.Name
            });

            await _dbContext.SaveChangesAsync();
            
            return Ok();
        }
    }

    public class ProfileDto
    {
        public string Name { get; set; }
    }
}