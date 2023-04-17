using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindRegardsApi.Data.Migrations
{
    public partial class DeviceStickerHasOneStickerRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stickers_DeviceStickers_DeviceStickerEntityId",
                table: "Stickers");

            migrationBuilder.DropIndex(
                name: "IX_Stickers_DeviceStickerEntityId",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "DeviceStickerEntityId",
                table: "Stickers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeviceStickerEntityId",
                table: "Stickers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stickers_DeviceStickerEntityId",
                table: "Stickers",
                column: "DeviceStickerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stickers_DeviceStickers_DeviceStickerEntityId",
                table: "Stickers",
                column: "DeviceStickerEntityId",
                principalTable: "DeviceStickers",
                principalColumn: "Id");
        }
    }
}
