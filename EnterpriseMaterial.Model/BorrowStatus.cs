using System.ComponentModel.DataAnnotations;

namespace EnterpriseMaterial.Model
{
    /// <summary>
    /// 申请状态表
    /// </summary>
    public class BorrowStatus
    {

        [Key]
        public int ID { get; set; }
        /// <summary>
        /// :代仓库审批
        /// 待部门领导审批
        /// 申请通过
        /// 驳回申请
        /// 取消申请
        /// </summary>
        public string Name { get; set; }
    }
}
