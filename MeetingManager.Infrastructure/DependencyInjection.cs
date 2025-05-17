using MeetingManager.Domain.Entity;
using MeetingManager.Domain.Interfaces.Repositories;
using MeetingManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MSSQL");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.InitRepositories();
        }
        private static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Room>, BaseRepository<Room>>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddScoped<IBaseRepository<Booking>, BaseRepository<Booking>>();
            services.AddScoped<IBookingRepository, BookingRepository>();

        }
    }
}
