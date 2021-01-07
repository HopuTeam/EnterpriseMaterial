using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.Dto.DepartmentDTO
{
    public class DepartmentOutput
    {
        public int Id { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string LeaderId { get; set; }
        public string ParentId { get; set; }
        public DateTime? AddTime { get; set; }
        //
        public string LeaderName { get; set; }
        //
        public string ParentIdName { get; set; }
    }
}
