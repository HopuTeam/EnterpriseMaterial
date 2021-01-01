using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 借还信息表
    /// </summary>
    public class Borrow
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// Type表外键
        /// </summary>
        public int TypeID { get; set; }
        /// <summary>
        /// Goods表外键
        /// </summary>
        public int GoodsID { get; set; }
        /// <summary>
        /// User表外键
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 申请理由
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string Suggest { get; set; }
        /// <summary>
        /// BorrowStatus表外键，表示流程状态
        /// </summary>
        public int StatusID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? SendTime { get; set; }
        /// <summary>
        /// 中间时间，需要二次审批才填
        /// </summary>
        public DateTime? MiddleTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Complete { get; set; }
        /// <summary>
        /// Department表外键
        /// </summary>
        public int DepartmentID { get; set; }
    }
}
