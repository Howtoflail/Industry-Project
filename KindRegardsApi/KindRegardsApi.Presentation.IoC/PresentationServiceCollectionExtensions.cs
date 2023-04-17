using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using KindRegardsApi.Common.Mapping;
using KindRegardsApi.Domain;
using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Domain.Stickers;
using KindRegardsApi.Presentation.DTO.Pets;
using KindRegardsApi.Presentation.DTO.Stickers;
using KindRegardsApi.Presentation.DTO.Messages;
using KindRegardsApi.Presentation.DTO.User;

namespace KindRegardsApi.Presentation.IoC
{
    public static class PresentationServiceCollectionExtensions
    {
        public static void AddPresentationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(mapperConfig =>
            {
                mapperConfig.AddProfile(new BidirectionalMappingProfile<DeviceSticker, StickerDTO>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<Sticker, UpdateStickerDTO>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<Pet, PetDTO>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<Pet, CreatePetDTO>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<Pet, GetAllPetDTO>());

                mapperConfig.AddProfile(new BidirectionalMappingProfile<Message, MessageDTO>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<Gift, GiftDTO>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<WhitelistItem, WhitelistItemDTO>());
                mapperConfig.AddProfile(new BidirectionalMappingProfile<User, UserDTO>());
            });
        }
    }
}
