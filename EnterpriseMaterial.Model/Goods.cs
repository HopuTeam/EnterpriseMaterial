using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 物品表
    /// </summary>
    public class Goods
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        /// <summary>
        /// 规格/200ml
        /// </summary>
        public string Specification { get; set; }
        /// <summary>
        /// 物品说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 单位/个
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 安全库存
        /// </summary>
        public int WarningNum { get; set; }
        /// <summary>
        /// 商品状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Type表外键
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime EntryTime { get; set; }
    }
}
