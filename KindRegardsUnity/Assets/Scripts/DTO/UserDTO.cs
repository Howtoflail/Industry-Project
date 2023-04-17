using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DTO
{
    public class UserDTO : MonoBehaviour
    {
        public int Id { get;  set; } 
        public int diary_code { get;  set; }
        public string computer_code { get;  set; }
        public PetDTO pet { get; set; }

    }
}