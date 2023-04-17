using KindRegardsApi.Domain;
using KindRegardsApi.Presentation.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindRegardsApi.Presentation.Mapping
{
    public class UserMapper
    {
        public UserDTO ToDTO(User user) 
        {
            return new UserDTO(
                user.id,
                user.diary_code,
                user.computer_code
                );
        }
        public User ToUser(UserDTO user)
        {
            return new User(
                user.id,
                user.diary_code,
                user.computer_code
                );
        }

        public List<UserDTO> toDTOS(List<User> users)
        {
            var dtos = new List<UserDTO>();
            foreach (User user in users )
            {
                dtos.Add( ToDTO(user) );
            }
            return dtos;
        }
    }
}
