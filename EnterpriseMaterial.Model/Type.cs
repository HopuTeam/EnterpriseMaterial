using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 审批流模板
    /// </summary>
    public class Type
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 设备借取，耗材领用
        /// </summary>
        public string Name { get; set; }
    }
}
