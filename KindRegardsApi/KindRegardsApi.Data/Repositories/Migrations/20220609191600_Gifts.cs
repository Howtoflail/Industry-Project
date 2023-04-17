using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindRegardsApi.Data.Migrations
{
    public partial class Gifts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Messages",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeviceId",
                table: "Messages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<long>(
                name: "GiftId",
                table: "Messages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StickerId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_GiftId",
                table: "Messages",
                column: "GiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Gifts_GiftId",
                table: "Messages",
                column: "GiftId",
                principalTable: "Gifts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Gifts_GiftId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Messages_GiftId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "GiftId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Read",
                table: "Messages");
        }
    }
}
