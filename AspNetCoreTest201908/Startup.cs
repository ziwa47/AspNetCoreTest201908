using AspNetCoreTest201908.Api.Lab01_IConfiguration;
using AspNetCoreTest201908.Api.Lab05_Service;
using AspNetCoreTest201908.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTest201908
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Lab04
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemory");
            });
                    
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Lab01
            services.Configure<ServerHost>(Configuration.GetSection("Server"));

            //Lab03
            services.AddHttpContextAccessor();
            services.AddSession();
            
            //Lab05
            services.AddScoped<IHttpService, HttpService>();
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            //Lab03
            app.UseSession();
            
            app.UseMvc();
        }
    }
}