using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Data.Configurations
{
    public class GiftConfiguration : IEntityTypeConfiguration<GiftEntity>
    {
        public void Configure(EntityTypeBuilder<GiftEntity> builder)
        {
        }
    }
}
