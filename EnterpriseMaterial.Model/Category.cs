using System.ComponentModel.DataAnnotations;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 分类表
    /// </summary>
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}