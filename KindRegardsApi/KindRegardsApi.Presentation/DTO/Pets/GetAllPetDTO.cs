namespace KindRegardsApi.Presentation.DTO.Pets
{
    public class GetAllPetDTO
    {
        public int Id { get; set; } = 0;
        public int petKind { get; set; } = 0;
        public string Name { get; set; } = "";
        public int Colour { get; set; } = 0;
        public int userId { get; set; } = 0;

        // Default constructor
        public GetAllPetDTO() { }

        public GetAllPetDTO(int Id, int petKind, string name, int colour)
        {
            this.Id = Id;
            this.petKind = petKind;
            this.Name = name;
            this.Colour = colour;
        }
        public GetAllPetDTO(int Id, int userId, int petKind, string name, int colour)
        {
            this.Id = Id;
            this.userId = userId;
            this.petKind = petKind;
            this.Name = name;
            this.Colour = colour;
        }
    }
}
