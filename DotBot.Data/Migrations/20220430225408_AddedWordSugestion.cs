using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotBot.Data.Migrations
{
    public partial class AddedWordSugestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordSuggestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SuggestedBy = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SwearWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locale = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordSuggestions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordSuggestions");
        }
    }
}
