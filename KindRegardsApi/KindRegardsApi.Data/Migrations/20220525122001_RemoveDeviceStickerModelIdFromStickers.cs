using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindRegardsApi.Data.Migrations
{
    public partial class RemoveDeviceStickerModelIdFromStickers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stickers_DeviceStickers_DeviceStickerModelId",
                table: "Stickers");

            migrationBuilder.RenameColumn(
                name: "DeviceStickerModelId",
                table: "Stickers",
                newName: "DeviceStickerEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Stickers_DeviceStickerModelId",
                table: "Stickers",
                newName: "IX_Stickers_DeviceStickerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stickers_DeviceStickers_DeviceStickerEntityId",
                table: "Stickers",
                column: "DeviceStickerEntityId",
                principalTable: "DeviceStickers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stickers_DeviceStickers_DeviceStickerEntityId",
                table: "Stickers");

            migrationBuilder.RenameColumn(
                name: "DeviceStickerEntityId",
                table: "Stickers",
                newName: "DeviceStickerModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Stickers_DeviceStickerEntityId",
                table: "Stickers",
                newName: "IX_Stickers_DeviceStickerModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stickers_DeviceStickers_DeviceStickerModelId",
                table: "Stickers",
                column: "DeviceStickerModelId",
                principalTable: "DeviceStickers",
                principalColumn: "Id");
        }
    }
}
