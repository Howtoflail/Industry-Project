using AutoMapper;
using KindRegardsApi.Domain;
using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Logic.Abstractions.Services;
using KindRegardsApi.Presentation.DTO.Messages;
using KindRegardsApi.Presentation.DTO.Pets;
using KindRegardsApi.Presentation.DTO.User;
using KindRegardsApi.Presentation.Mapping;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindRegardsApi.Presentation.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private IMapper mapper;
        private UserMapper uMapper = new UserMapper();
        private IUserService uService;

        public UserController(IMapper mapper, IUserService uService)
        {
            this.mapper = mapper;
            this.uService = uService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var item = await this.uService.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            return this.uMapper.ToDTO(item);
        }
        //http://127.0.0.1:8080/api/v1/user/computer_code
        [HttpGet("{computer_code}")]
        public async Task<ActionResult<UserDTO>> GetUserComputer_Code([FromRoute] string computer_code)
        {
            var user = await this.uService.findUser(computer_code);

            if (user == null)
            {
                throw new ArgumentException("No user found!");
            }

            return this.mapper.Map<UserDTO>(user);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var whitelist = await this.uService.GetAll();

            return this.uMapper.toDTOS(whitelist);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO user)
        {
            if (user.diary_code.ToString().Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'name' must contain a value."}
                };

                return BadRequest(errors);
            }

            var createdWhitelistItem = await this.uService.Create(uMapper.ToUser(user));

            if (createdWhitelistItem == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not create whitelist item. Please try again later."}
                };

                return BadRequest(errors);
            }

            return CreatedAtAction("CreateUser", this.mapper.Map<UserDTO>(createdWhitelistItem));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserDTO>> UpdateWhitelistItem(UserDTO UserDTO, int id)
        {
            if (UserDTO.id != id)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'id' does not match with the resource id."}
                };

                return BadRequest(errors);
            }

            if (UserDTO.diary_code.ToString().Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'name' must contain a value."}
                };

                return BadRequest(errors);
            }

            if (await this.GetUser(UserDTO.id) == null)
            {
                return NotFound();
            }

            var updatedItem = await this.uService.Update(uMapper.ToUser(UserDTO));

            if (updatedItem == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not update whitelist item. Please try again later."}
                };

                return BadRequest(errors);
            }

            return this.mapper.Map<UserDTO>(updatedItem);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (await this.GetUser(id) == null)
            {
                return NotFound();
            }
            var item = await this.uService.Get(id);
            if (!await this.uService.Delete(item))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
