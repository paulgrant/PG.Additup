using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApi.Migrations
{
    public partial class AddedScoreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "Score",
               columns: table => new
               {
                   userId = table.Column<Guid>(nullable: false),
                   level = table.Column<int>(nullable: false),
                   highScore = table.Column<int>(nullable: false),

               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Score", x => x.userId);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "Score");
        }
    }
}
