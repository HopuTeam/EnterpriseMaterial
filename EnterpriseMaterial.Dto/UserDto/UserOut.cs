using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.Dto.UserDto
{
    public class UserOut
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Status { get; set; }
        public string Birthday { get; set; }
        public int Sex { get; set; }
        public int IdentityID { get; set; }
        public DateTime? EntryTime { get; set; }
    }
}