using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
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
        public string Description { get; set; }
        public decimal Money { get; set; }
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
        public int TypeID { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
