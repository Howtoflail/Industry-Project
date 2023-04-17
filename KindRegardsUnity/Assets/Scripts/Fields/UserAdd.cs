using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Fields
{
    class UserAdd
    {
        public int diary_code { get; }
        public string computer_code { get; }

        // Default constructor
        public UserAdd() { }

        public UserAdd(int diary_code, string computer_code)
        {
            this.diary_code = diary_code;
            this.computer_code = computer_code;
        }
    }
}
