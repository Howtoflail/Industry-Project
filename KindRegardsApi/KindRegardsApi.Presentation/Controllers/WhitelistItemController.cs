using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using KindRegardsApi.Presentation.Mapping;
using KindRegardsApi.Presentation.DTO.Messages;
using KindRegardsApi.Logic.Abstractions.Services;
using KindRegardsApi.Domain.Messages;

namespace KindRegardsApi.Presentation.Controllers
{
    [ApiController]
    [Route("whitelist")]
    public class WhitelistItemController : ControllerBase
    {
        private IMapper mapper;
        private WhitelistItemMapper whitelistItemMapper = new WhitelistItemMapper();
        private IWhitelistItemService whitelistService;

        public WhitelistItemController(IMapper mapper, IWhitelistItemService whitelistService)
        {
            this.mapper = mapper;
            this.whitelistService = whitelistService;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<WhitelistItemDTO>> GetWhitelistItem(long id)
        {
            var item = await this.whitelistService.Get(id);

            if (item == null)
            {
                return NotFound();
            }

            return this.whitelistItemMapper.ToDTO(item);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WhitelistItemDTO>>> GetWhitelist()
        {
            var whitelist = await this.whitelistService.GetAll();

            return this.whitelistItemMapper.toDTOS(whitelist);
        }

        [HttpPost]
        public async Task<ActionResult<WhitelistItemDTO>> CreateWhitelistItem(WhitelistItemDTO whitelistItemDTO)
        {
            if (whitelistItemDTO.Text.Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'text' must contain a value."}
                };

                return BadRequest(errors);
            }

            var createdWhitelistItem = await this.whitelistService.Create( whitelistItemDTO.Text);

            if (createdWhitelistItem == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not create whitelist item. Please try again later."}
                };

                return BadRequest(errors);
            }

            return CreatedAtAction("CreateWhitelistItem", this.mapper.Map<WhitelistItemDTO>(createdWhitelistItem));
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<WhitelistItemDTO>> UpdateWhitelistItem(WhitelistItemDTO whitelistItemDTO, long id)
        {
            if (whitelistItemDTO.Id != id)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'id' does not match with the resource id."}
                };

                return BadRequest(errors);
            }

            if (whitelistItemDTO.Text.Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'text' must contain a value."}
                };

                return BadRequest(errors);
            }

            if (await this.GetWhitelistItem(whitelistItemDTO.Id) == null)
            {
                return NotFound();
            }

            var itemToCreate = this.mapper.Map<WhitelistItem>(whitelistItemDTO);
            var updatedItem = await this.whitelistService.Update(itemToCreate);

            if (updatedItem == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not update whitelist item. Please try again later."}
                };

                return BadRequest(errors);
            }

            return this.mapper.Map<WhitelistItemDTO>(updatedItem);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteWhitelistItem(long id)
        {
            if (await this.GetWhitelistItem(id) == null)
            {
                return NotFound();
            }

            if (!await this.whitelistService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
