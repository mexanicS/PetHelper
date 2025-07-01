using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelper.VolunteerRequests.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VolunteerRequests");

            migrationBuilder.CreateTable(
                name: "VolunteerRequests",
                schema: "VolunteerRequests",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    admin_id = table.Column<Guid>(type: "uuid", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    volunteer_info = table.Column<string>(type: "text", nullable: true),
                    request_status = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    rejected_comment = table.Column<string>(type: "text", nullable: false),
                    discussion_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer_requests", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerRequests",
                schema: "VolunteerRequests");
        }
    }
}
