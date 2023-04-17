using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Fields
{
    internal class User
    {   
        public int id { get; }
        public Pet pet { get; }
        public int diary_code { get; } 
        public string computer_code { get; } 

        // Default constructor
        public User() { }

        public User(int id, int diary_code, Pet pet, string computer_code)
        {
            this.id = id;
            this.pet = pet;
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
        public User(int id, int diary_code, string computer_code)
        {
            this.id = id;
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
        public User(int diary_code, string computer_code)
        {
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
    }
}
