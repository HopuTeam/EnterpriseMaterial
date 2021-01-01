using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Sex { get; set; }
        public int SignID { get; set; }
        public int DepartmentID { get; set; }
        public DateTime? EntryTime { get; set; }
        public bool Status { get; set; }
        public DateTime? LockTime { get; set; }
    }
}