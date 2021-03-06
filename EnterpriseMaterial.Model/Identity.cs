﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 身份表
    /// </summary>
    public class Identity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 状态，表示是否禁用
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// 禁用时间
        /// </summary>
        public DateTime? LockTime { get; set; }
    }
}
