using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Logic.Abstractions.Services;
using AutoMapper;
using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity;
using KindRegardsApi.Entity.Messages;

namespace KindRegardsApi.Logic.Services
{
    public class MessageService : IMessageService
    {
        private IMapper mapper;
        private IMessageRepository messageRepository;
        private IWhitelistItemRepository whitelistRepository;
        private IDeviceRepository deviceRepository;

        public MessageService(
            IMapper mapper,
            IMessageRepository messageRepository,
            IWhitelistItemRepository whitelistItemRepository,
            IDeviceRepository deviceRepository
        ) {
            this.mapper = mapper;
            this.messageRepository = messageRepository;
            this.whitelistRepository = whitelistItemRepository;
            this.deviceRepository = deviceRepository;
        }

        // Empty list for whitelist
        List<WhitelistItemEntity> whitelist= new List<WhitelistItemEntity>();

        public async Task<Message?> Get(string deviceId,long id)
        {
            var messageEntity = await this.messageRepository.GetAsync(id);

            if (messageEntity == null)
            {
                return null;
            }

            messageEntity.Device = await this.deviceRepository.GetAsync(deviceId);;

            return this.mapper.Map<Message>(messageEntity);
        }

        public async Task<List<Message>> GetAll(string deviceId)
        {
            var list = new List<Message>();
            var messages = this.messageRepository.GetAll();

            if (messages == null)
            {
                return null;
            }

            foreach(var messageEntity in messages)
            {
                messageEntity.Device = await this.deviceRepository.GetAsync(deviceId);

                if (messageEntity.Device != null && messageEntity.Device.Id == deviceId)
                {
                    list.Add(this.mapper.Map<Message>(messageEntity));
                }
            }

            return list;
        }

        public async Task<Message?> Create(string deviceId,string text,DateTime date)
        {
            if (text.Length == 0)
            {
                throw new ArgumentException("Text must have a length that is larger than 0");
            }

            if (this.CheckWhitelist(text).Result)
            {
                var device = await this.deviceRepository.GetAsync(deviceId);

                if (device == null)
                {
                    device = new DeviceEntity(deviceId);
                }

                var entityToCreate = new MessageEntity(0L, device, text, date);
                var createdEntity = await this.messageRepository.CreateAsync(entityToCreate);

                if (createdEntity == null)
                {
                    return null;
                }

                return this.mapper.Map<Message>(createdEntity);
            }
            return null;
        }

        public async Task<Message?> Update(Message message,string deviceId)
        {
            if (!await this.messageRepository.HasWithId(message.Id))
            {
                return null;
            }

            // Only reply to other people, not to yourself
            if (message.DeviceId != deviceId)
            {
                // If the message has been read, and you want to thank the sender, use the ThankSender method
                if (message.Read&&message.Thanked)
                {
                    await this.ThankSender(message);
                }

                var entityToUpdate = this.mapper.Map<MessageEntity>(message);
                entityToUpdate.Device = await this.deviceRepository.GetAsync(deviceId);
                
                var updatedMessageEntity = await this.messageRepository.UpdateAsync(entityToUpdate);

                if (updatedMessageEntity == null)
                {
                    return null;
                }

                return this.mapper.Map<Message>(updatedMessageEntity);
            }

            return null;
        }

        public async Task<bool> Delete(long id)
        {
            if (await this.messageRepository.HasWithId(id))
            {
                return await this.messageRepository.DeleteAsync(id);
            }

            return false;
        }

        // After receiving a gift, the user is able to thank the sender of the gift
        public async Task ThankSender(Message message)
        {
            // Can only thank the sender if the message has been read
            if (message.Read == true)
            {
                message.Thanked = true;
            }
            return;
        }

        // Checks whitelist and returns true if the check passes
        public async Task<bool> CheckWhitelist(string message)
        {
            whitelist = whitelistRepository.GetAll();

            foreach (WhitelistItemEntity item in whitelist)
            {
                if (item.Text == message)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
