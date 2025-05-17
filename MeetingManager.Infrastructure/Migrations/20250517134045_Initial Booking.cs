using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeetingManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "Id", "BookedBy", "EndTime", "RoomId", "StartTime" },
                values: new object[,]
                {
                    { 1L, "Alex Martyn", new DateTime(2025, 5, 14, 11, 30, 0, 0, DateTimeKind.Unspecified), 1L, new DateTime(2025, 5, 14, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, "Maksym Luzan", new DateTime(2025, 5, 11, 11, 15, 0, 0, DateTimeKind.Unspecified), 3L, new DateTime(2025, 5, 11, 9, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, "Oksana Orlova", new DateTime(2025, 5, 17, 23, 15, 0, 0, DateTimeKind.Unspecified), 2L, new DateTime(2025, 5, 17, 21, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RoomId",
                table: "Booking",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");
        }
    }
}
