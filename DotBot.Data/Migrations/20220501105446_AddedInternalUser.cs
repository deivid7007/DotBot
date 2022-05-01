using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotBot.Data.Migrations
{
    public partial class AddedInternalUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuggestedBy",
                table: "WordSuggestions");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WordSuggestions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "WordSuggestions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "InternalUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NonCryptoCoins = table.Column<decimal>(type: "money", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordSuggestions_UserId",
                table: "WordSuggestions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WordSuggestions_InternalUser_UserId",
                table: "WordSuggestions",
                column: "UserId",
                principalTable: "InternalUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordSuggestions_InternalUser_UserId",
                table: "WordSuggestions");

            migrationBuilder.DropTable(
                name: "InternalUser");

            migrationBuilder.DropIndex(
                name: "IX_WordSuggestions_UserId",
                table: "WordSuggestions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WordSuggestions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WordSuggestions");

            migrationBuilder.AddColumn<decimal>(
                name: "SuggestedBy",
                table: "WordSuggestions",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
