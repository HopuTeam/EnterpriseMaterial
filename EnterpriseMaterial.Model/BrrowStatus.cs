using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    public class BrrowStatus
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
