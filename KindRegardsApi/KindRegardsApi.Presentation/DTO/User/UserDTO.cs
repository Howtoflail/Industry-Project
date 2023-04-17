using KindRegardsApi.Domain;
using KindRegardsApi.Presentation.DTO.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindRegardsApi.Presentation.DTO.User
{
    public class UserDTO
    {
        public int id { get; set; } = 0;
        public PetDTO? pet { get; set; } = null;
        public int diary_code { get; set; } = 0;
        public string computer_code { get; set; } = "";

        // Default constructor
        public UserDTO() { }

        public UserDTO(int id, int diary_code, PetDTO pet, string computer_code)
        {
            this.id = id;
            this.pet= pet;
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
        public UserDTO(int id, int diary_code, string computer_code)
        {
            this.id = id;
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
        public UserDTO( int diary_code, string computer_code)
        {
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
    }
}
