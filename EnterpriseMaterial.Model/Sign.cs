using System.ComponentModel.DataAnnotations;

namespace EnterpriseMaterial.Model
{
    public class Sign
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 身份ID
        /// </summary>
        public int IdentityID { get; set; }
    }
}
