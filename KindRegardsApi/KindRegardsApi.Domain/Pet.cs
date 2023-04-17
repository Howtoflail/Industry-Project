namespace KindRegardsApi.Domain
{
    public class Pet
    { 
        public int Id { get; set; } = 0;
        public int petKind { get; set; } = 0;
        public string Name { get; set; } = "";
        public int Colour { get; set; } = 0;
        public int userId { get; set; } = 0;

    // Default constructor
    public Pet() { }

    public Pet(int Id, int petKind, string name, int colour, int userId)
    {
        this.Id = Id;
        this.petKind = petKind;
        this.Name = name;
        this.Colour = colour;
            this.userId= userId;
    }
}
}
