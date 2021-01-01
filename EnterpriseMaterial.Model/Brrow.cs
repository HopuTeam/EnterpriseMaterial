using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 借还信息表
    /// </summary>
    public class Brrow
    {
        [Key]
        public int ID { get; set; }
        public int TypeID { get; set; }
        public int GoodsID { get; set; }
        public int UserID { get; set; }
        public string Description { get; set; }
        public string Suggest { get; set; }
        public int StatusID { get; set; }
        public DateTime? SendTime { get; set; }
        public DateTime? MiddleTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Number { get; set; }
        public bool Complete { get; set; }
        public int DepartmentID { get; set; }
    }
}
