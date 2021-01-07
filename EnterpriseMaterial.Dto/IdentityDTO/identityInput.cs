using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Dto.IdentityDTO
{
   public class identityInput
    {

        public int ID { get; set; }

        [Required]
        [StringLength(16)]
        public int RoleId { get; set; }

        [Required]
        [StringLength(16)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Description { get; set; }





    }
}
