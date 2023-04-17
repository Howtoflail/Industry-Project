using KindRegardsApi.Domain.Messages;
using KindRegardsApi.Presentation.DTO.Messages;

namespace KindRegardsApi.Presentation.Mapping
{
    public class GiftMapper
    {
        public GiftDTO ToDTO(Gift gift)
        {
            if (gift.StickerId==0)
            {
                throw new ArgumentNullException("Could not map gift since it does not reference any gifts");
            }

            return new GiftDTO(
                gift.Id,
                gift.StickerId
            );
        }

        public List<GiftDTO> toDTOS(List<Gift> gifts)
        {
            var dtos = new List<GiftDTO>();

            foreach (Gift gift in gifts)
            {
                dtos.Add(this.ToDTO(gift));
            }

            return dtos;
        }
    }
}
