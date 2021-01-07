using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 出生年月日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// Sign表外键，查询此用户的账号密码
        /// </summary>
        public int SignID { get; set; }
        /// <summary>
        /// Department表外键，此用户所属部门
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? EntryTime { get; set; }
        /// <summary>
        /// 状态，表示是否被禁用
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 禁用时间
        /// </summary>
        public DateTime? LockTime { get; set; }
    }
}