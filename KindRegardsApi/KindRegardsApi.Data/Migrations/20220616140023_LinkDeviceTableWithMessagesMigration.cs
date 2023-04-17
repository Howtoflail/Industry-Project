using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindRegardsApi.Data.Migrations
{
    public partial class LinkDeviceTableWithMessagesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "Messages",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_DeviceId",
                table: "Messages",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Devices_DeviceId",
                table: "Messages",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Devices_DeviceId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_DeviceId",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "Messages",
                keyColumn: "DeviceId",
                keyValue: null,
                column: "DeviceId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "Messages",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
