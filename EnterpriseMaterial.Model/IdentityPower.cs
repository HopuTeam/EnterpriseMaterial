using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    public class IdentityPower
    {
        [Key]
        public int ID { get; set; }
        public string IdentityID { get; set; }
        public string PowerID { get; set; }
    }
}