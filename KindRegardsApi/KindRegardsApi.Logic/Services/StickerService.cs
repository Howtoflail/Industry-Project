using AutoMapper;

using KindRegardsApi.Domain.Stickers;
using KindRegardsApi.Logic.Abstractions.Services;
using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity;
using KindRegardsApi.Entity.Stickers;

namespace KindRegardsApi.Logic.Services
{
    public class StickerService : IStickerService
    {
        private IMapper mapper;
        private IStickerRepository stickerRepository;
        private IDeviceStickerRepository deviceStickerRepository;
        private IDeviceRepository deviceRepository;

        public StickerService(
            IMapper mapper,
            IStickerRepository stickerRepository,
            IDeviceStickerRepository deviceStickerRepository,
            IDeviceRepository deviceRepository
        ) {
            this.mapper = mapper;
            this.stickerRepository = stickerRepository;
            this.deviceStickerRepository = deviceStickerRepository;
            this.deviceRepository = deviceRepository;
        }

        public async Task<DeviceSticker?> Get(string deviceId, long id)
        {
            var stickerEntity = await this.stickerRepository.GetAsync(id);
            var deviceStickerEntity = await this.deviceStickerRepository.GetByStickerAndDeviceId(deviceId, id);

            if (stickerEntity == null)
            {
                return null;
            }

            return this.ConstructCompositeSticker(stickerEntity, deviceStickerEntity);
        }

        public async Task<List<DeviceSticker>> GetAll(string deviceId)
        {
            var deviceStickers = new List<DeviceSticker>();

            foreach (StickerEntity stickerEntity in this.stickerRepository.GetAll())
            {
                var deviceStickerEntity = await this.deviceStickerRepository.GetByStickerAndDeviceId(
                    deviceId,
                    stickerEntity.Id
                );

                if (deviceStickerEntity != null)
                {
                    deviceStickerEntity.Device = await this.deviceRepository.GetAsync(deviceId);
                }

                var deviceSticker = this.ConstructCompositeSticker(stickerEntity, deviceStickerEntity);
                deviceStickers.Add(deviceSticker);
            }

            return deviceStickers;
        }

        public async Task<Sticker?> Create(string image)
        {
            if (image.Length == 0)
            {
                throw new ArgumentException("Image must have a length that is larger than 0");
            }

            var entityToCreate = new StickerEntity(0L, image);
            var createdEntity = await this.stickerRepository.CreateAsync(entityToCreate);

            if (createdEntity == null)
            {
                return null;
            }

            return this.mapper.Map<Sticker>(createdEntity);
        }

        public async Task<Sticker?> Update(Sticker sticker)
        {
            if (!await this.stickerRepository.HasWithId(sticker.Id))
            {
                return null;
            }

            var entityToUpdate = this.mapper.Map<StickerEntity>(sticker);
            var updatedStickerEntity = await this.stickerRepository.UpdateAsync(entityToUpdate);

            if (updatedStickerEntity == null)
            {
                return null;
            }

            return this.mapper.Map<Sticker>(updatedStickerEntity);
        }

        public async Task<bool> Delete(long id)
        {
            if (await this.stickerRepository.HasWithId(id))
            {
                return await this.stickerRepository.DeleteAsync(id);
            }

            return false;
        }

        public async Task<DeviceSticker?> Unlock(string deviceId, long stickerId, int amount)
        {
            var stickerEntity = await this.stickerRepository.GetAsync(stickerId);

            if (stickerEntity == null)
            {
                return null;
            }

            var existingDeviceSticker = await this.deviceStickerRepository.GetByStickerAndDeviceId(deviceId, stickerId);

            if (existingDeviceSticker == null)
            {
                var deviceStickerToCreate = new DeviceStickerEntity(
                    0L,
                    await this.GetDeviceEntity(deviceId),
                    stickerEntity, amount
                );

                existingDeviceSticker = await this.deviceStickerRepository.CreateAsync(deviceStickerToCreate);
            }
            else
            {
                existingDeviceSticker.Amount = amount;
                existingDeviceSticker = await this.deviceStickerRepository.UpdateAsync(existingDeviceSticker);
            }

            return this.mapper.Map<DeviceSticker>(existingDeviceSticker);
        }

        private DeviceSticker ConstructCompositeSticker(StickerEntity sticker, DeviceStickerEntity? deviceSticker)
        {
            // Return the queried device sticker when it has been found
            if (deviceSticker != null)
            {
                deviceSticker.Sticker = sticker;
                return this.mapper.Map<DeviceSticker>(deviceSticker);
            }

            // Return a mostly empty device sticker since the device hasn't unlocked the actual sticker yet
            return new DeviceSticker(
                sticker.Id,
                "",
                this.mapper.Map<Sticker>(sticker),
                0
            );
        }

        private async Task<DeviceEntity> GetDeviceEntity(string deviceId)
        {
            var entity = await this.deviceRepository.GetAsync(deviceId);

            if (entity != null)
            {
                return entity;
            }

            return new DeviceEntity(deviceId);
        }
    }
}
