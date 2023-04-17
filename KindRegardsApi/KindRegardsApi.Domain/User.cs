using System;
using System.Collections.Generic;

namespace KindRegardsApi.Domain
{
    public class User
    {
        public int id { get; set; } = 0;
        public Pet pet { get; set; } = null;
        public int diary_code { get; set; } = 0;
        public string computer_code { get; set; } = "";

        // Default constructor
        public User() { }

        public User(int id, int diary_code, Pet pet, string computer_code)
        {
            this.id = id;
            this.pet= pet;
            this.diary_code = diary_code;
            this.computer_code= computer_code;
        }
        public User(int id, int diary_code, string computer_code)
        {
            this.id = id;
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
        public User( int diary_code, string computer_code)
        {
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
    }
}
