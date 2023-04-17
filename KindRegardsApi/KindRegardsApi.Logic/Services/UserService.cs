using KindRegardsApi.Domain;
using KindRegardsApi.Logic.Abstractions.Services;
using AutoMapper;
using KindRegardsApi.Data.Abstractions.Repositories;
using KindRegardsApi.Entity.user;
using KindRegardsApi.Entity;
using KindRegardsApi.Domain.Messages;
using System.Collections.Generic;

namespace KindRegardsApi.Logic.Services
{
    public class UserService : IUserService
    {
        private IMapper mapper;
        private IUserRepository userRepo;
        private IPetService pService;

        public UserService(IMapper mapper, IUserRepository userRepo, IPetService pService)
        {
            this.mapper = mapper;
            this.userRepo = userRepo;
            this.pService = pService;
        }

        public async Task<User?> Create(User user)
        {
            var userEntity = this.mapper.Map<UserEntity>(user);

            var createdEntity = await this.userRepo.CreateAsync(userEntity);

            if(createdEntity == null)
            {
                return null;
            }
            return this.mapper.Map<User>(createdEntity);
        }

        public async Task<bool> Delete(User user)
        {
            var userEntity = this.mapper.Map<UserEntity>(user);
            if (await this.userRepo.GetAsync(user.id) != null)
            {
                return await this.userRepo.DeleteAsync(userEntity);
            }
            return false;
        }

        public async Task<User?> findUser(string computer_code)
        {
            var userEntity = await this.userRepo.GetAsyncComputerCode(computer_code);

            if (userEntity == null)
            {
                return null;
            }

            return this.mapper.Map<User>(userEntity);
        }

        public async Task<User?> Get(int id)
        {
            var userEntity = await this.userRepo.GetAsync(id);

            if (userEntity == null)
            {
                return null;
            }
            
            //get the users
            User u = this.mapper.Map<User>(userEntity);
            //get the fitting pet
            Pet p = await pService.Get(u.id);
            return new User(u.id, u.diary_code, p, u.computer_code);
            
        }

        public async Task<List<User>> GetAll()
        {
            var list = new List<User>();
            var users =  this.userRepo.GetAll();

            if (users == null)
            {
                return null;
            }

            foreach (var UserEntity in users)
            {
                //get the users
                User u = this.mapper.Map<User>(UserEntity);
                //get the fitting pet
                var    p= await this.pService.Get(u.id);
                User user = new User(u.id, u.diary_code, p, u.computer_code);
                list.Add(user);
            }

            return list;
        }

        public async Task<User?> Update(User user)
        {
            var existingUser = await this.Get(user.id);

            if (existingUser == null)
            {
                return null;
            }

            var userToUpdate = this.mapper.Map<UserEntity>(user);

            var updatedUser = await this.userRepo.UpdateAsync(userToUpdate);

            if (updatedUser == null)
            {
                return null;
            }

            return this.mapper.Map<User>(updatedUser);
        }

    }
}
