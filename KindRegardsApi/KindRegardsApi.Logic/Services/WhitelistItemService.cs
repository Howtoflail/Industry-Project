using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Logic.Abstractions.Services;
using AutoMapper;
using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Logic.Services
{
    public class WhitelistItemService : IWhitelistItemService
    {
        private IMapper mapper;
        private IWhitelistItemRepository whitelistRepository;

        public WhitelistItemService(IMapper mapper, IWhitelistItemRepository whitelistRepository)
        {
            this.mapper = mapper;
            this.whitelistRepository = whitelistRepository;
        }

        public async Task<WhitelistItem?> Create(string text)
        {
            if (text.Length == 0)
            {
                throw new ArgumentException("Text must have a length that is larger than 0");
            }

            var entityToCreate = new WhitelistItemEntity(0L, text);
            var createdEntity = await this.whitelistRepository.CreateAsync(entityToCreate);

            if (createdEntity == null)
            {
                    return null;
            }

            return this.mapper.Map<WhitelistItem>(createdEntity);
        }

        public async Task<bool> Delete(long id)
        {
            if (await this.whitelistRepository.HasWithId(id))
            {
                return await this.whitelistRepository.DeleteAsync(id);
            }

            return false;
        }

        public async Task<WhitelistItem?> Get(long id)
        {
            var whitelistItemEntity = await this.whitelistRepository.GetAsync(id);

            if (whitelistItemEntity == null)
            {
                return null;
            }

            return this.mapper.Map<WhitelistItem>(whitelistItemEntity);
        }

        public async Task<List<WhitelistItem>> GetAll()
        {
            var list = new List<WhitelistItem>();
            var whitelist = this.whitelistRepository.GetAll();

            if (whitelist == null)
            {
                return null;
            }

            foreach (var whitelistItemEntity in whitelist)
            {
                list.Add(this.mapper.Map<WhitelistItem>(whitelistItemEntity));
            }

            return list;
        }

        public async Task<bool> IsWhitelisted(string text)
        {
            var whiteListedWords = await this.GetAll();

            return whiteListedWords.Find(w => w.Text == text) != null;
        }

        public Task<WhitelistItem?> Update(WhitelistItem whitelistItem)
        {
            throw new NotImplementedException();
        }
    }
}
