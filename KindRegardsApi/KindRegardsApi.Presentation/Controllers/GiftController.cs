using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using KindRegardsApi.Presentation.Mapping;
using KindRegardsApi.Presentation.DTO.Messages;
using KindRegardsApi.Logic.Abstractions.Services;
using KindRegardsApi.Domain.Messages;
namespace KindRegardsApi.Presentation.Controllers
{
    [ApiController]
    [Route("gifts")]
    public class GiftController : ControllerBase
    {
        private IMapper mapper;
        private GiftMapper giftMapper = new GiftMapper();
        private IGiftService giftService;

        public GiftController(IMapper mapper, IGiftService giftService)
        {
            this.mapper = mapper;
            this.giftService = giftService;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<GiftDTO>> GetGift(long id)
        {
            var gift= await this.giftService.Get(id);

            if (gift == null)
            {
                return NotFound();
            }

            return this.giftMapper.ToDTO(gift);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDTO>>> GetGifts()
        {
            var gifts = await this.giftService.GetAll();

            return this.giftMapper.toDTOS(gifts);
        }

        [HttpPost]
        public async Task<ActionResult<GiftDTO>> CreateGift(GiftDTO giftDTO)
        {
            if (giftDTO.StickerId==0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'sticker' must contain a value."}
                };

                return BadRequest(errors);
            }

            var createdGift = await this.giftService.Create(giftDTO.StickerId);

            if (createdGift == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not create gift. Please try again later."}
                };

                return BadRequest(errors);
            }

            return CreatedAtAction("CreateGift", this.mapper.Map<GiftDTO>(createdGift));
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<GiftDTO>> UpdateGift(GiftDTO giftDTO, long id)
        {
            if (giftDTO.Id != id)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'id' does not match with the resource id."}
                };

                return BadRequest(errors);
            }

            if (giftDTO.StickerId==0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'sticker' must contain a value."}
                };

                return BadRequest(errors);
            }

            if (await this.GetGift(giftDTO.Id) == null)
            {
                return NotFound();
            }

            var giftToCreate = this.mapper.Map<Gift>(giftDTO);
            var updatedGift = await this.giftService.Update(giftToCreate);

            if (updatedGift == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not update gift. Please try again later."}
                };

                return BadRequest(errors);
            }

            return this.mapper.Map<GiftDTO>(updatedGift);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteGift(long id)
        {
            if (await this.GetGift(id) == null)
            {
                return NotFound();
            }

            if (!await this.giftService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
