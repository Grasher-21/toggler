using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TogglerAPI.Database;
using TogglerAPI.Interfaces;

namespace TogglerAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationSection SQLConnectionString = Configuration.GetSection("Database").GetSection("ConnectionString");

            services.AddDbContext<TogglerDbContext>(options => options.UseSqlServer(SQLConnectionString.Value));
            //services.AddIdentity<TogglerContext, IdentityRole>().AddEntityFrameworkStores<TogglerContext>().AddDefaultTokenProviders();

            services.AddEntityFrameworkSqlServer();
            services.AddScoped<ITogglerDbContext, TogglerDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddSingleton<ISQLConnection>(x => new SQLConnection(SQLConnectionString.Value));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
