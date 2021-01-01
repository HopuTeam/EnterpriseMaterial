using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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