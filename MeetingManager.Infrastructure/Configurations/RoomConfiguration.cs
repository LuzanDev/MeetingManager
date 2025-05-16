using MeetingManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Infrastructure.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Capacity).IsRequired();

            builder.HasData(
                new Room { Id = 1, Name = "Small Room", Capacity = 10 },
                new Room { Id = 2, Name = "Medium Room", Capacity = 25 },
                new Room { Id = 3, Name = "Large Room", Capacity = 50 }
                );
        }
    }
}
