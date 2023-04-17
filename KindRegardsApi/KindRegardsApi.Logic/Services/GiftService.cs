using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Logic.Abstractions.Services;
using AutoMapper;
using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Logic.Services
{
    public class GiftService : IGiftService
    {
        private IMapper mapper;
        private IGiftRepository giftRepository;

        public GiftService(IMapper mapper, IGiftRepository giftRepository)
        {
            this.mapper = mapper;
            this.giftRepository = giftRepository;
        }

        public async Task<Gift?> Create(long stickerId)
        {
            if (stickerId==0)
            {
                throw new ArgumentException("The gift must have a sticker attached.");
            }

            var entityToCreate = new GiftEntity(0L, stickerId);
            var createdEntity = await this.giftRepository.CreateAsync(entityToCreate);

            if (createdEntity == null)
            {
                return null;
            }

            return this.mapper.Map<Gift>(createdEntity);
        }

        public async Task<bool> Delete(long id)
        {
            if (await this.giftRepository.HasWithId(id))
            {
                return await this.giftRepository.DeleteAsync(id);
            }

            return false;
        }

        public async Task<Gift?> Get(long id)
        {
            var giftEntity = await this.giftRepository.GetAsync(id);

            if (giftEntity == null)
            {
                return null;
            }

            return this.mapper.Map<Gift>(giftEntity);
        }

        public async Task<List<Gift>> GetAll()
        {
            var list = new List<Gift>();
            var gifts = this.giftRepository.GetAll();

            if (gifts == null)
            {
                return null;
            }

            foreach (var giftEntity in gifts)
            {
                list.Add(this.mapper.Map<Gift>(giftEntity));
            }

            return list;
        }

        public async Task<Gift?> Update(Gift gift)
        {
            if (!await this.giftRepository.HasWithId(gift.Id))
            {
                return null;
            }

            var entityToUpdate = this.mapper.Map<GiftEntity>(gift);
            var updatedGiftEntity = await this.giftRepository.UpdateAsync(entityToUpdate);

            if (updatedGiftEntity == null)
            {
                return null;
            }

            return this.mapper.Map<Gift>(updatedGiftEntity);
        }
    }
}
