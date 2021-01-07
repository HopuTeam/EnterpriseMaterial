using EnterpriseMaterial.Data;
using EnterpriseMaterial.Dto.DepartmentDTO;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EnterpriseMaterial.Logic
{
    public class DepartmentService : IDepartmentService
    {
        #region 构造函数注入
        private readonly ILogger<DepartmentService> _logger;
        private readonly CoreEntities _dbContext;
        public DepartmentService(ILogger<DepartmentService> logger, CoreEntities dbcontext)
        {
            _logger = logger;
            _dbContext = dbcontext;
        }
        public int Add(DepartmentInput inputEntity)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentOutput> GetList()
        {
            return (from a in _dbContext.Set<Departments>()
                    join b in _dbContext.Set<User>() on a.UserID equals b.ID into join_a
                    from c in join_a.DefaultIfEmpty()
                    select new DepartmentOutput
                    {
                        DepartmentId =Convert.ToString(a.ID),
                        DepartmentName = a.Name,
                        Id = a.ID,
                        LeaderId = Convert.ToString(a.UserID),
                        ParentId = Convert.ToString(a.ParentID),
                        LeaderName = c.Name,

                    }).ToList();
        }

        public List<DepartmentParentOutput> GetReviewerMsg(string userId)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentOutput> LoadEntities(int id)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentOutput> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo)
        {
            throw new NotImplementedException();
        }

        public int Update(DepartmentInput inputEntity)
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
