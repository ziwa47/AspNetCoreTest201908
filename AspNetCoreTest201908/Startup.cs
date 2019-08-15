using AspNetCoreTest201908.Entity;
using AspNetCoreTest201908.Model;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            //Lab05
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemory");
            });

            //Lab01
            services.AddLogging();

            //Lab02
            services.Configure<ServerHost>(Configuration.GetSection("Server"));

            //Lab04
            services.AddHttpContextAccessor();
            services.AddSession();
            
            //Lab06
            services.AddScoped<IHttpService, HttpService>();
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            //Lab04
            app.UseSession();
            
            app.UseMvc();
        }
    }
}