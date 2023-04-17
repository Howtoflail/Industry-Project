using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KindRegardsApi.Entity;

namespace KindRegardsApi.Data.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<PetEntity>
    {
        public void Configure(EntityTypeBuilder<PetEntity> builder)
        {
        }
    }
}
