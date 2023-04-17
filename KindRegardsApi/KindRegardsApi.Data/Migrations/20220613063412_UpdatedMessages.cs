using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindRegardsApi.Data.Migrations
{
    public partial class UpdatedMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Thanked",
                table: "Messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thanked",
                table: "Messages");
        }
    }
}
