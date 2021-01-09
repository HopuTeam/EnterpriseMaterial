using System;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 部门表
    /// </summary>
    public class Department
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 部门名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentID { get; set; }
        /// <summary>
        /// 部门负责人/主管
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? EntryTime { get; set; }
    }
}
