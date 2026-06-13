using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddfsdParticipante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Sessions_SessionId",
                table: "Participant");

            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Sessions_SessionId1",
                table: "Participant");

            migrationBuilder.DropIndex(
                name: "IX_Participant_SessionId1",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "SessionId1",
                table: "Participant");

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionId",
                table: "Participant",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Sessions_SessionId",
                table: "Participant",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Sessions_SessionId",
                table: "Participant");

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionId",
                table: "Participant",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
                name: "FK_Participant_Sessions_SessionId",
                table: "Participant",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Sessions_SessionId1",
                table: "Participant",
                column: "SessionId1",
                principalTable: "Sessions",
                principalColumn: "Id");
        }
    }
}
