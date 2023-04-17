using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KindRegardsApi.Entity.Stickers;

namespace KindRegardsApi.Data.Configurations
{
    public class DeviceStickerConfiguration : IEntityTypeConfiguration<DeviceStickerEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceStickerEntity> builder)
        {
            // DeviceSticker -> Sticker relationship
            builder.HasOne(x => x.Sticker)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Device)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
