using AutoMapper;

using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Domain;
using KindRegardsApi.Entity;
using KindRegardsApi.Logic.Abstractions.Services;

namespace KindRegardsApi.Logic.Services
{
    public class PetService : IPetService
    {
        private IMapper mapper;
        private IDeviceRepository deviceRepository;
        private IPetRepository petRepository;

        public PetService(IMapper mapper, IDeviceRepository deviceRepository, IPetRepository petRepository)
        {
            this.mapper = mapper;
            this.deviceRepository = deviceRepository;
            this.petRepository = petRepository;
        }

        public async Task<Pet?> Get(int userId)
        {
            var petEntity = await this.petRepository.GetAsync(userId);

            if (petEntity == null)
            {
                return null;
            }

            return this.mapper.Map<Pet>(petEntity);
        }

        public async Task<List<Pet>> GetAll()
        {
            var entities = await this.petRepository.GetAll();
            return this.mapper.Map<List<Pet>>(entities);
        }

        public async Task<Pet?> Create(Pet pet, int userId)
        {
            var petEntity = this.mapper.Map<PetEntity>(pet);
            petEntity.userId = userId;

            var createdEntity = await this.petRepository.CreateAsync(petEntity);

            if (createdEntity == null)
            {
                return null;
            }

            return this.mapper.Map<Pet>(createdEntity);
        }

        public async Task<Pet?> Update(Pet pet, int userId)
        {
            var existingPet = await this.Get(pet.userId);

            if (existingPet == null)
            {
                return null;
            }

            var petEntity = this.mapper.Map<PetEntity>(pet);
            petEntity.userId = userId;

            var updatedPet = await this.petRepository.UpdateAsync(petEntity);

            if (updatedPet == null)
            {
                return null;
            }

            return this.mapper.Map<Pet>(updatedPet);
        }

        public async Task<bool> Delete(int id)
        {
            if (await this.petRepository.GetAsync(id) != null)
            {
                return await this.petRepository.DeleteAsync(id);
            }

            return false;
        }


    }
}
