using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 入库信息表
    /// </summary>
    public class Obtain
    {
        [Key]
        public int ID { get; set; }
        public int GoodsID { get; set; }
        public int Number { get; set; }
        public DateTime EntryTime { get; set; }
        public int UserID { get; set; }
    }
}