﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelper.Accounts.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_refresh_sessions_user_user_id",
                table: "refresh_sessions");

            migrationBuilder.AddForeignKey(
                name: "fk_refresh_sessions_users_user_id",
                table: "refresh_sessions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_refresh_sessions_users_user_id",
                table: "refresh_sessions");

            migrationBuilder.AddForeignKey(
                name: "fk_refresh_sessions_user_user_id",
                table: "refresh_sessions",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
