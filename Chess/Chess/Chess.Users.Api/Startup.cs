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
using Chess.Users.Models.SettingsModels;
using Chess.Middlewares;

namespace Chess.UsersService
{
    public class Startup
    {
        private readonly ChessUsersSettings _settings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _settings = Configuration.GetSection(typeof(ChessUsersSettings).Name).Get<ChessUsersSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chess Users API", Version = "v1" });
            });
            #endregion

            services.AddSingleton(_settings);

            #region Dependeny injections
            services.AddUnitOfWork();
            services.AddUserServices();
            services.AddUtilities();
            services.AddDbContext<UsersDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("UsersDbConnection"), providerOptions => providerOptions.CommandTimeout(60));
            });
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

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
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