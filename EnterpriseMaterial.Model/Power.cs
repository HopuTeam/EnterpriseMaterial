﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        /// 
        /// </summary>
        public string MenuIcon { get; set; }
        /// <summary>
        /// 菜单路径
        /// </summary>
        public string ActionUrl { get; set; }
    }
}
