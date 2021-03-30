using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using web.api.App.Common;
using web.api.App.Recipes;
using web.api.App.Users;
using web.api.DataAccess;

namespace web.api.Helpings
{
    [ExcludeFromCodeCoverage]
    public static class DiMapper
    {
        public static void Map(IServiceCollection services, IConfiguration configuration)
        {
            // CONFIGURATION
            services.AddSingleton(configuration);
            var authConfig = configuration.GetSection("Auth").Get<AuthConfig>();
            services.AddCors();

            // SERVICES
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "web.api", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.DescribeAllParametersInCamelCase();
            });
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            // AUTH

            services.AddAuthentication(options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false; // TODO: enable in Prod
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(authConfig.SecretJwtKey),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true
                    };
                });
            // Hangfire
            services.AddHangfire(c => { c.UseMemoryStorage(); });

            // Register DbContext
            AppDbContext.Register(services);

            // BUSINESS SERVICES
            services.AddTransient<RecipeService>();
            services.AddTransient<UserService>();

            // CONTROLLERS
            services.AddTransient<RecipeController>();
            services.AddTransient<UserController>();
        }
    }
}