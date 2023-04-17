using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Data.Repositories;

namespace KindRegardsApi.Data.IoC
{
    public static class DataServiceCollectionExtensions
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Entity framework
            var connectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            // Add repositories
            services.AddScoped<IStickerRepository, StickerRepository>();
            services.AddScoped<IDeviceStickerRepository, DeviceStickerRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IGiftRepository, GiftRepository>();
            services.AddScoped<IWhitelistItemRepository, WhitelistItemRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
