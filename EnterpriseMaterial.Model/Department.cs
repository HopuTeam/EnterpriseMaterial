using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        /// <summary>
        /// 部门负责人/主管
        /// </summary>
        public int UserID { get; set; }
        public DateTime? EntryTime { get; set; }
    }
}
