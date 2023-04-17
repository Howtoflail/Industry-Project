using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using KindRegardsApi.Domain;
using KindRegardsApi.Presentation.Attributes;
using KindRegardsApi.Presentation.DTO.Pets;
using KindRegardsApi.Logic.Abstractions.Services;

namespace KindRegardsApi.Presentation.Controllers
{
    [ApiController]
    [Route("pet")]
    public class PetController : ControllerBase
    {
        private IMapper mapper;
        private IPetService petService;
        private IWhitelistItemService whitelistItemService;

        public PetController(
            IMapper mapper,
            IPetService petService,
            IWhitelistItemService whitelistItemService
        ) {
            this.mapper = mapper;
            this.petService = petService;
            this.whitelistItemService = whitelistItemService;
        }

        //http://127.0.0.1:8080/api/v1/pet
        [HttpGet]
        public async Task<ActionResult<List<GetAllPetDTO>>> GetAll()
        {
            var pets = await this.petService.GetAll();
            return this.mapper.Map<List<GetAllPetDTO>>(pets);
        }


        //http://127.0.0.1:8080/api/v1/pet/userId
        [HttpGet("{userId}")]
        public async Task<ActionResult<PetDTO>> GetPet([FromRoute] int userId)
        {
            var pet = await this.petService.Get(userId);

            if(pet == null)
            {
                return NotFound();
            }

            return this.mapper.Map<PetDTO>(pet);
        }

        [HttpPost]
        public async Task<ActionResult<PetDTO>> CreatePet(CreatePetDTO dto)
        {

            //if (!await this.whitelistItemService.IsWhitelisted(dto.Name))
            //{
            //    var errors = new Dictionary<string, string>()
            //    {
            //        {"Message", "Field 'text' contains text that is not whitelisted."}
            //    };

            //    return StatusCode(403, errors);
            //}

            var petToCreate = this.mapper.Map<Pet>(dto);

            var createdPet = await this.petService.Create(petToCreate, petToCreate.userId);

            if (createdPet == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not create sticker. Please try again later."}
                };

                return BadRequest(errors);
            }

            return this.mapper.Map<PetDTO>(createdPet);
        }

        [HttpPut("{id:int}")]
        [RequireDeviceIdHeader]
        public async Task<ActionResult<PetDTO>> UpdatePet(PetDTO dto, int id)
        {
            int userId = Convert.ToInt32(Request.Headers["userId"]);

            if (!await this.whitelistItemService.IsWhitelisted(dto.Name))
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'text' contains text that is not whitelisted."}
                };

                return StatusCode(403, errors);
            }

            if (dto.Id != id)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'id' does not match with the resource id."}
                };

                return BadRequest(errors);
            }

            var existingPet = await this.petService.Get(userId);

            if (existingPet == null)
            {
                return NotFound();
            }

            var petToCreate = this.mapper.Map<Pet>(dto);
            petToCreate.userId = Convert.ToInt32(userId);

            var updatedPet = await this.petService.Update(petToCreate, userId);

            if (updatedPet == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not update sticker. Please try again later."}
                };

                return BadRequest(errors);
            }

            return this.mapper.Map<PetDTO>(updatedPet);
        }

        [HttpDelete("{userId:int}")]
        [RequireDeviceIdHeader]
        public async Task<ActionResult> DeletePet(int userId)
        {

            if (await this.petService.Get(userId) == null)
            {
                return NotFound();
            }

            if (!await this.petService.Delete(userId))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
