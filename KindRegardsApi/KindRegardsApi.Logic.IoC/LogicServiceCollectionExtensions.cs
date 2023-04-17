using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using KindRegardsApi.Common.Mapping;
using KindRegardsApi.Entity;
using KindRegardsApi.Entity.Stickers;
using KindRegardsApi.Domain;
using KindRegardsApi.Domain.Stickers;
using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Logic.Services;
using KindRegardsApi.Logic.Abstractions.Services;
using KindRegardsApi.Entity.Messages;
using KindRegardsApi.Entity.user;

namespace KindRegardsApi.Logic.IoC
{
    public static class LogicServiceCollectionExtensions
    {
        public static void AddLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.CreateMap<DeviceEntity, string>().ConvertUsing(d => d.Id);
                mapperConfig.CreateMap<string, DeviceEntity>();

                mapperConfig.AddProfile(new BidirectionalMappingProfile<StickerEntity, Sticker>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<PetEntity, Pet>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<DeviceStickerEntity, DeviceSticker>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<MessageEntity, Message>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<GiftEntity, Gift>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<WhitelistItemEntity, WhitelistItem>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<UserEntity, User>());
            });

            services.AddScoped<IStickerService, StickerService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IGiftService, GiftService>();
            services.AddScoped<IWhitelistItemService, WhitelistItemService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
