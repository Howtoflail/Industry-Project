using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Data.Configurations
{
    internal class WhitelistItemConfiguration : IEntityTypeConfiguration<WhitelistItemEntity>
    {
        public void Configure(EntityTypeBuilder<WhitelistItemEntity> builder)
        {
        }
    }
}
