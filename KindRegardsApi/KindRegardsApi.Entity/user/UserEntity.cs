using System;
namespace KindRegardsApi.Entity.user
{

    public class UserEntity
    {
        public int id { get; set; } = 0;
        public int diary_code { get; set; } = 0;
        public string computer_code { get; set; } = "";

        // Default constructor
        public UserEntity() { }

        public UserEntity(int id, int diary_code, string computer_code)
        {
            this.id = id;
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }

        public UserEntity( int diary_code, string computer_code)
        {
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
    }
}
