using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Data.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<MessageEntity>
    {
        public void Configure(EntityTypeBuilder<MessageEntity> builder)
        {
        }
    }
}
