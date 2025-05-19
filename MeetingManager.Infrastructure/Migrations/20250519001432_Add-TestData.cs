using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 25, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 25, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 29, 11, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 29, 9, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 30, 23, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 30, 21, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "Id", "BookedBy", "EndTime", "RoomId", "StartTime" },
                values: new object[] { 17L, "Oksana Orlova", new DateTime(2025, 5, 29, 14, 45, 0, 0, DateTimeKind.Unspecified), 2L, new DateTime(2025, 5, 29, 12, 15, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 14, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 14, 10, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 11, 11, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 11, 9, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 5, 17, 23, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 17, 21, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
