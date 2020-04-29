using Chess.ApiGateway.Api.ApiServices;
using Chess.ApiGateway.Api.ApiServices.UsersService;
using Chess.ApiGateway.Api.Infrastructure.Settings;
using Chess.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using System;
using System.Text;

namespace Chess.ApiGateway
{
    public class Startup
    {
        private readonly ApiGatewaySettings _settings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _settings = Configuration.GetSection(typeof(ApiGatewaySettings).Name).Get<ApiGatewaySettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
         
            #region Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chess Users API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });
            #endregion

            #region JWT Authentication setup
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    // This should be delete when in production
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _settings.JwtSettings.Issuer,
                        ValidAudiences = _settings.JwtSettings.Audiences,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSettings.Key))
                    };
                });
            #endregion

            services.AddRefitClient<IAuthService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(_settings.ApiUrls.AuthUrl));
            
            services.AddRefitClient<IUsersService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(_settings.ApiUrls.UsersServiceUrl));
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

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chess Api Gateway v1");
            });
        }
    }
}