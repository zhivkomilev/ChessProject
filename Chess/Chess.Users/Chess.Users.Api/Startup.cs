using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Chess.Users.DataAccess;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Chess.Users.Services.Infrastructure.AutoMapper;
using Chess.Users.DataAccess.Infrastructure.ServicesExtensions;
using Chess.Users.Services.Infrastructure.Services;
using Chess.Users.Utilities.Infrastructure;

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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chess Users API", Version = "v1" });
            });

            services.AddUnitOfWork();
            services.AddUserServices();
            services.AddUtilities();

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chess Users API v1");
            });
        }
    }
}