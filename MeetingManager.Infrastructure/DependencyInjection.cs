using MeetingManager.Application.Interfaces.Services;
using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Interfaces.Repositories;
using MeetingManager.Infrastructure.Identity;
using MeetingManager.Infrastructure.Repositories;
using MeetingManager.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeetingManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MSSQL");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddJwtAuthentication(configuration);
            services.InitServices();
            services.InitRepositories();
        }
        private static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Room>, BaseRepository<Room>>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<IBaseRepository<Booking>, BaseRepository<Booking>>();
            services.AddScoped<IBookingRepository, BookingRepository>();
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
        private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse(); 
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var json = JsonSerializer.Serialize(new { errorMessage = "Invalid token" });
                        return context.Response.WriteAsync(json);
                    }
                };
            });

            services.AddAuthorization();

        }
    }
}
