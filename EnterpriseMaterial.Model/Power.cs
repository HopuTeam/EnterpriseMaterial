using System.ComponentModel.DataAnnotations;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Power
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 菜单图标(使用Layui图标 Class name)
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string ActionUrl { get; set; }
    }
}
