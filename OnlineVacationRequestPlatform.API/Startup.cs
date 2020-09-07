using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineVacationRequestPlatform.BusinessLayer.Interfaces;
using OnlineVacationRequestPlatform.BusinessLayer.Services;
using OnlineVacationRequestPlatform.BusinessLayer.Utilities;
using OnlineVacationRequestPlatform.DataLayer;
using OnlineVacationRequestPlatform.DataLayer.Interfaces;
using OnlineVacationRequestPlatform.DataLayer.Repositories;

namespace OnlineVacationRequestPlatform.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddAutoMapper(c => c.AddProfile<MappingProfiles>(), typeof(Startup));
       
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IVacationRequestRepository, VacationRequestRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IVacationRequestService, VacationRequestService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
