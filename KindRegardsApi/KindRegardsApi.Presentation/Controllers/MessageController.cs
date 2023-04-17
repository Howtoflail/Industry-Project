using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

using KindRegardsApi.Presentation.Mapping;
using KindRegardsApi.Presentation.DTO.Messages;
using KindRegardsApi.Logic.Abstractions.Services;
using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Presentation.Attributes;

namespace KindRegardsApi.Presentation.Controllers
{
    [ApiController]
    [Route("messages")]
    public class MessageController : ControllerBase
    {
        private IMapper mapper;
        private MessageMapper messageMapper = new MessageMapper();
        private IMessageService messageService;

        public MessageController(IMapper mapper, IMessageService messageService, IWhitelistItemService whitelistItemService)
        {
            this.mapper = mapper;
            this.messageService = messageService;
        }

        [HttpGet("{id:long}")]
        [RequireDeviceIdHeader]
        public async Task<ActionResult<MessageDTO>> GetMessage(long id)
        {
            var deviceId = Request.Headers["deviceId"];
            var message = await this.messageService.Get( deviceId,id);

            if (message== null)
            {
                return NotFound();
            }

            return this.messageMapper.ToDTO(message);
        }

        [HttpGet]
        [RequireDeviceIdHeader]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
            var deviceId = Request.Headers["deviceId"];
            var messages = await this.messageService.GetAll(deviceId);

            return this.messageMapper.toDTOS(messages);
        }

        [HttpPost]
        [RequireDeviceIdHeader]
        public async Task<ActionResult<MessageDTO>> CreateMessage(MessageDTO messageDTO)
        {
            if (messageDTO.Text.Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'text' must contain a value."}
                };

                return BadRequest(errors);
            }

            if (!await this.messageService.CheckWhitelist(messageDTO.Text))
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'text' contains text that is not whitelisted."}
                };

                return StatusCode(403, errors);
            }

            var deviceId = Request.Headers["deviceId"];
            var createdMessage = await this.messageService.Create(deviceId, messageDTO.Text, messageDTO.Date);

            if (createdMessage == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not create message. Please try again later."}
                };

                return BadRequest(errors);
            }

            return CreatedAtAction("CreateMessage", this.mapper.Map<MessageDTO>(createdMessage));
        }

        [HttpPut("{id:long}")]
        [RequireDeviceIdHeader]
        public async Task<ActionResult<MessageDTO>> UpdateMessage(MessageDTO messageDTO, long id)
        {
            if (messageDTO.Id != id)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'id' does not match with the resource id."}
                };

                return BadRequest(errors);
            }

            if (messageDTO.Text.Length == 0)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Field 'text' must contain a value."}
                };

                return BadRequest(errors);
            }

            if (await this.GetMessage(messageDTO.Id) == null)
            {
               return NotFound();
            }
            var deviceId = Request.Headers["deviceId"];
            var messageToCreate = this.mapper.Map<Message>(messageDTO);
            var updatedMessage = await this.messageService.Update(messageToCreate,deviceId);

            if (updatedMessage == null)
            {
                var errors = new Dictionary<string, string>()
                {
                    {"Message", "Could not update message. Please try again later."}
                };

                return BadRequest(errors);
            }

            return this.mapper.Map<MessageDTO>(updatedMessage);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> DeleteMessage(long id)
        {
            if (await this.GetMessage(id) == null)
            {
                return NotFound();
            }

            if (!await this.messageService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
