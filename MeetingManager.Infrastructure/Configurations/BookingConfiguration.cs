using MeetingManager.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Infrastructure.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.BookedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.StartTime)
                .IsRequired();

            builder.Property(b => b.EndTime)
                .IsRequired();

            builder.HasOne(b => b.Room)
                .WithMany() 
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Booking 
                { 
                    Id = 1, 
                    RoomId = 1, 
                    StartTime = new DateTime (2025,5,14,10,0,0),
                    EndTime = new DateTime(2025, 5, 14, 11, 30, 0),
                    BookedBy = "Alex Martyn"
                },
                new Booking
                {
                    Id = 2,
                    RoomId = 3,
                    StartTime = new DateTime(2025, 5, 11, 9, 15, 0),
                    EndTime = new DateTime(2025, 5, 11, 11, 15, 0),
                    BookedBy = "Maksym Luzan"
                },
                new Booking
                {
                    Id = 3,
                    RoomId = 2,
                    StartTime = new DateTime(2025, 5, 17, 21, 0, 0),
                    EndTime = new DateTime(2025, 5, 17, 23, 15, 0),
                    BookedBy = "Oksana Orlova"
                }
                );
        }
    }
}
