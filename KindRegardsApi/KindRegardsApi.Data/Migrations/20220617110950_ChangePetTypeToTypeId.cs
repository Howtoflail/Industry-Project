using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindRegardsApi.Data.Migrations
{
    public partial class ChangePetTypeToTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Pets");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Pets");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Pets",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
