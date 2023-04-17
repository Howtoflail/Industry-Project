using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using KindRegardsApi.Presentation.Mapping;
using KindRegardsApi.Presentation.Attributes;
using KindRegardsApi.Presentation.DTO.Stickers;
using KindRegardsApi.Logic.Abstractions.Services;
using KindRegardsApi.Domain.Stickers;

namespace KindRegardsApi.Presentation.Controllers
{
    [ApiController]
    [Route("stickers")]
    public class StickerController : ControllerBase
    {
        private IMapper mapper;
        private StickerMapper stickerMapper = new StickerMapper();
        private IStickerService stickerService;

        public StickerController(IMapper mapper, IStickerService stickerService)
        {
            this.mapper = mapper;
            this.stickerService = stickerService;
        }

        [HttpGet("{id:long}")]
        [RequireDeviceIdHeader]
        public async Task<ActionResult<StickerDTO>> GetSticker(long id)
        {
            var deviceId = Request.Headers["deviceId"];
            var deviceSticker = await this.stickerService.Get(deviceId, id);

            if(deviceSticker == null)
            {
                return NotFound();
            }

            return this.stickerMapper.ToDTO(deviceSticker);
        }

        [HttpGet]
        [RequireDeviceIdHeader]
        public async Task<ActionResult<IEnumerable<StickerDTO>>> GetStickers()
        {
            var deviceId = Request.Headers["deviceId"];
            var deviceStickers = await this.stickerService.GetAll(deviceId);

            return this.stickerMapper.toDTOS(deviceStickers);
        }

        [HttpPost]
        public async Task<ActionResult<UpdateStickerDTO>> CreateSticker(CreateStickerDTO stickerDTO)
        {
            if (stickerDTO.Image.Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'image' must contain a value."}
                };

                return BadRequest(errors);
            }

            var createdSticker = await this.stickerService.Create(stickerDTO.Image);

            if (createdSticker == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not create sticker. Please try again later."}
                };

                return BadRequest(errors);
            }

            return CreatedAtAction("CreateSticker", this.mapper.Map<UpdateStickerDTO>(createdSticker));
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<UpdateStickerDTO>> UpdateSticker(UpdateStickerDTO stickerDTO, long id)
        {
            if (stickerDTO.Id != id)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'id' does not match with the resource id."}
                };

                return BadRequest(errors);
            }

            if (stickerDTO.Image.Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'image' must contain a value."}
                };

                return BadRequest(errors);
            }

            if (await this.GetSticker(stickerDTO.Id) == null)
            {
                return NotFound();
            }

            var stickerToCreate = this.mapper.Map<Sticker>(stickerDTO);
            var updatedSticker = await this.stickerService.Update(stickerToCreate);

            if (updatedSticker == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not update sticker. Please try again later."}
                };

                return BadRequest(errors);
            }

            return this.mapper.Map<UpdateStickerDTO>(updatedSticker);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteSticker(long id)
        {
            if (await this.GetSticker(id) == null)
            {
                return NotFound();
            }

            if (!await this.stickerService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("unlock")]
        [RequireDeviceIdHeader]
        public async Task<ActionResult<StickerDTO>> UnlockSticker(UnlockStickerDTO stickerDTO)
        {
            if (await this.GetSticker(stickerDTO.Id) == null)
            {
                return NotFound();
            }

            var deviceId = Request.Headers["deviceId"];
            var unlockedDeviceSticker = await this.stickerService.Unlock(deviceId, stickerDTO.Id, stickerDTO.Amount);

            if (unlockedDeviceSticker == null)
            {
                return NotFound();
            }

            return this.stickerMapper.ToDTO(unlockedDeviceSticker);
        }
    }
}
