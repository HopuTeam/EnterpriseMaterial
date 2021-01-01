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
        /// <summary>
        /// 物品id
        /// </summary>
        public int GoodsID { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime EntryTime { get; set; }
        /// <summary>
        /// User表外键，入库操作人
        /// </summary>
        public int UserID { get; set; }
    }
}