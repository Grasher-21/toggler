using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TogglerAPI.BusinessCore;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;
using TogglerAPI.Repositories;
using TogglerAPI.Utilities;

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

            services.AddDbContext<TogglerContext>(options => options.UseSqlServer(SQLConnectionString.Value));

            services.AddEntityFrameworkSqlServer();

            services.AddScoped<ILogger>(x => new Logger(Configuration.GetSection("LoggerFile").Value));

            services.AddScoped<IHeaderValidation, HeaderValidation>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IToggleService, ToggleService>();
            services.AddScoped<IToggleServicePermissionService, ToggleServicePermissionService>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IToggleRepository, ToggleRepository>();
            services.AddScoped<IToggleServicePermissionRepository, ToggleServicePermissionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
