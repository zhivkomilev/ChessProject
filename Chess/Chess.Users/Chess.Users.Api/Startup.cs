using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Chess.Users.DataAccess;
using Chess.Users.DataAccess.Repositories;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Chess.Users.Utilities;
using Chess.Users.Utilities.Interfaces;
using AutoMapper;
using Chess.Users.Services.Infrastructure;
using Chess.Users.Services.EntityServices;
using Chess.Users.Services.EntityServices.Interfaces;

namespace Chess.UsersService
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
            services.AddControllers();
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("UsersDbConnection"));
            });

            #region Singleton registrations
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            #endregion

            #region Scoped registrations
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(ServiceMappingProfile));
            #endregion


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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chess Users API");
            });

        }
    }
}
