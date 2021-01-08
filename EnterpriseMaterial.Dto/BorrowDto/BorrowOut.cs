using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.Dto.BorrowDto
{
    public class BorrowOut
    {
        public int ID { get; set; }
        public string GoodsName { get; set; }
        public int Goodsid { get; set; }
        public string UserName { get; set; }
        public int Userid { get; set; }
        public string Description { get; set; }
        public string StatusName { get; set; }
        public string Suggest { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 中间时间，需要二次审批才填
        /// </summary>
        public DateTime? MiddleTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        public string Complete { get; set; }
        public decimal price { get; set; }
    }
}
