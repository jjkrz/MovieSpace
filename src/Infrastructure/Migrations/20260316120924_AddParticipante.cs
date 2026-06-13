using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SessionId1",
                table: "Participant",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participant_SessionId1",
                table: "Participant",
                column: "SessionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Sessions_SessionId1",
                table: "Participant",
                column: "SessionId1",
                principalTable: "Sessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Sessions_SessionId1",
                table: "Participant");

            migrationBuilder.DropIndex(
                name: "IX_Participant_SessionId1",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "SessionId1",
                table: "Participant");
        }
    }
}
