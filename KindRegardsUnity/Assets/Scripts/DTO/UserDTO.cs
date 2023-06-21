using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DTO
{
    public class UserDTO : MonoBehaviour
    {
        //store user id from firebase in a local file
        //check if the user that is playing the game has the file on his system and if the id exists
        //if it doesnt exist prompt the user to create a user
        public int Id { get;  set; } 
        public int diary_code { get;  set; }
        public string computer_code { get;  set; }
        public PetDTO pet { get; set; }

    }
}