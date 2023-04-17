using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindRegardsApi.Data.Migrations
{
    public partial class AddDeviceStickerToStickerRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeviceStickerModelId",
                table: "Stickers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StickerId",
                table: "DeviceStickers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Stickers_DeviceStickerModelId",
                table: "Stickers",
                column: "DeviceStickerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStickers_StickerId",
                table: "DeviceStickers",
                column: "StickerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStickers_Stickers_StickerId",
                table: "DeviceStickers",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stickers_DeviceStickers_DeviceStickerModelId",
                table: "Stickers",
                column: "DeviceStickerModelId",
                principalTable: "DeviceStickers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStickers_Stickers_StickerId",
                table: "DeviceStickers");

            migrationBuilder.DropForeignKey(
                name: "FK_Stickers_DeviceStickers_DeviceStickerModelId",
                table: "Stickers");

            migrationBuilder.DropIndex(
                name: "IX_Stickers_DeviceStickerModelId",
                table: "Stickers");

            migrationBuilder.DropIndex(
                name: "IX_DeviceStickers_StickerId",
                table: "DeviceStickers");

            migrationBuilder.DropColumn(
                name: "DeviceStickerModelId",
                table: "Stickers");

            migrationBuilder.AlterColumn<long>(
                name: "StickerId",
                table: "DeviceStickers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
