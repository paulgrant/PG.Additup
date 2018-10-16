using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    exerciseId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    leftNumber = table.Column<int>(nullable: false),
                    rightNumber = table.Column<int>(nullable: false),
                    mathOperator = table.Column<int>(nullable: false),
                    answer = table.Column<string>(nullable: true),
                    correctAnswerGiven = table.Column<bool>(nullable: false),
                    userId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.exerciseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exercise");
        }
    }
}
