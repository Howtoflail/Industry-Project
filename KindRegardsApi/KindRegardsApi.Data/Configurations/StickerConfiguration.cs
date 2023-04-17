using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KindRegardsApi.Entity.Stickers;

namespace KindRegardsApi.Data.Configurations
{
    public class StickerConfiguration : IEntityTypeConfiguration<StickerEntity>
    {
        public void Configure(EntityTypeBuilder<StickerEntity> builder)
        {
            builder.Ignore("DeviceStickerEntityId");
        }
    }
}
