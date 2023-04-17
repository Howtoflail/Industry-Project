using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Presentation.DTO.Messages;

namespace KindRegardsApi.Presentation.Mapping
{
    public class MessageMapper
    {
        public MessageDTO ToDTO(Message message)
        {

            return new MessageDTO(
                message.Id,
                message.DeviceId,
                message.Text,
                message.Date
            );
        }

        public List<MessageDTO> toDTOS(List<Message> messages)
        {
            var dtos = new List<MessageDTO>();

            foreach (Message message in messages)
            {
                dtos.Add(this.ToDTO(message));
            }

            return dtos;
        }
    }
}
