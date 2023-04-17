using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindRegardsApi.Data.Migrations
{
    public partial class DeviceStickerHasOneStickerRelationshipDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStickers_Stickers_StickerId",
                table: "DeviceStickers");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStickers_Stickers_StickerId",
                table: "DeviceStickers",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStickers_Stickers_StickerId",
                table: "DeviceStickers");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStickers_Stickers_StickerId",
                table: "DeviceStickers",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id");
        }
    }
}
