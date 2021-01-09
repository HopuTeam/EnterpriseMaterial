using System;

namespace EnterpriseMaterial.Dto.DepartmentDTO
{
    public class DepartmentOutput
    {
        public int Id { get; set; }
        public string DepartmentId { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
        /// <summary>
        /// 部门负责人/主管
        /// </summary>
        public int UserID { get; set; }
        public DateTime? EntryTime { get; set; }
        //
        public string LeaderName { get; set; }
        //
        public string ParentIdName { get; set; }
    }
}
