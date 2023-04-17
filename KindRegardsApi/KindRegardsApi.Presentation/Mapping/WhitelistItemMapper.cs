using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Presentation.DTO.Messages;

namespace KindRegardsApi.Presentation.Mapping
{
    public class WhitelistItemMapper
    {
        public WhitelistItemDTO ToDTO(WhitelistItem whitelistItem)
        {

            return new WhitelistItemDTO(
                whitelistItem.Id,
                whitelistItem.Text
            );
        }

        public List<WhitelistItemDTO> toDTOS(List<WhitelistItem> whitelist)
        {
            var dtos = new List<WhitelistItemDTO>();

            foreach (WhitelistItem whitelistItem in whitelist)
            {
                dtos.Add(this.ToDTO(whitelistItem));
            }

            return dtos;
        }
    }
}
