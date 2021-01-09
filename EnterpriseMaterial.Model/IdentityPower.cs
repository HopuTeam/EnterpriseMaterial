using System.ComponentModel.DataAnnotations;

namespace EnterpriseMaterial.Model
{
    public class IdentityPower
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public int IdentityID { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public int PowerID { get; set; }
    }
}