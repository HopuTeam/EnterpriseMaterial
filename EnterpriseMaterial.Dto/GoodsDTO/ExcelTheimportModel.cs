using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.Dto.GoodsDTO
{
  public  class ExcelTheimportModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 商品类型
        /// </summary>
        public string Category { get; set; }
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
        /// 耗材领用或设备借取
        /// </summary>
        public string Type { get; set; }
    }
}
