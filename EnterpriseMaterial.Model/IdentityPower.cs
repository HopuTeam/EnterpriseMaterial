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
        public int IdentityID { get; set; }
        public int PowerID { get; set; }
    }
}