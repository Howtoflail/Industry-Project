using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Fields
{
    internal class Pet
    {
        public int Id { get; }
        public PetStateEnum petKind { get; set; } 
        public string Name { get; }
        public string Colour { get; } 
        public int userId { get;  } 

        // Default constructor
        public Pet() { }

        public Pet(int Id, PetStateEnum petKind, string name, string colour, int userId)
        {
            this.Id = Id;
            this.petKind = petKind;
            this.Name = name;
            this.Colour = colour;
            this.userId = userId;
        }
        public Pet(int Id, PetStateEnum petKind, string name, string colour)
        {
            this.Id = Id;
            this.petKind = petKind;
            this.Name = name;
            this.Colour = colour;
        }
        public Pet(PetStateEnum petKind, string name, string colour)
        {
            this.petKind = petKind;
            this.Name = name;
            this.Colour = colour;
        }
    }
}
