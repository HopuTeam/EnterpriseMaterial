using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.Dto.GoodsDTO
{
   public class GoodsViewModelDTO: ExcelTheimportModel
    {
        /// <summary>
        /// 物品状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 入库时间
        /// </summary>
        public DateTime EntryTime { get; set; }
    }
}
