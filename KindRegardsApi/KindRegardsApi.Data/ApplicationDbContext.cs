using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using KindRegardsApi.Entity;
using KindRegardsApi.Entity.Stickers;
using KindRegardsApi.Entity.Messages;
using KindRegardsApi.Data.Configurations;
using KindRegardsApi.Entity.user;

namespace KindRegardsApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<DeviceEntity>? Devices {get; set;} = null;
        public DbSet<PetEntity>? pet {get; set;} = null;
        public DbSet<StickerEntity>? Stickers {get; set;} = null;
        public DbSet<DeviceStickerEntity>? DeviceStickers {get; set;} = null;
        public DbSet<MessageEntity>? Messages { get; set; } = null;
        public DbSet<GiftEntity>? Gifts { get; set; } = null;
        public DbSet<UserEntity>? user { get; set; } = null;
        public DbSet<WhitelistItemEntity>?  whitelist { get; set; } = null;
         

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // TODO:: Configure here...

            // Auto-migrate the database
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DeviceStickerConfiguration());
            modelBuilder.ApplyConfiguration(new StickerConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new GiftConfiguration());
            modelBuilder.ApplyConfiguration(new WhitelistItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }

    // IMPORT CLASS BELOW - THIS IS USED WHEN THE MIGRATIONS ARE RUNNING
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../KindRegardsApi.Host/appsettings.json")
                                                    .Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = configuration.GetConnectionString("Default");
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
