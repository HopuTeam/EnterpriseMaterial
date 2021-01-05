using System;
using System.Collections.Generic;
using System.Text;
using EnterpriseMaterial.Dto.IdentityDTO;
using EnterpriseMaterial.ILogic;
using Microsoft.EntityFrameworkCore;
using EnterpriseMaterial.Model;
using System.Linq;
using EnterpriseMaterial.Data;
using Microsoft.Extensions.Logging;

namespace EnterpriseMaterial.Logic
{
    public class IdentityBLL : IIdentityBLL
    {
        private readonly ILogger<IdentityBLL> logger;
        private readonly CoreEntities coreEntities;
        #region 构造函数注入


        public IdentityBLL(ILogger<IdentityBLL> logger,CoreEntities coreEntities)//
        {
            this.logger = logger;
            this.coreEntities = coreEntities;
        }

       
        #endregion







        //实现接口 并在这里处理数据
        public List<IdentityQutput> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo)
        {
            IQueryable<Identity> iquery;

            if (selectInfo == null || selectInfo == "")
            {
                totalCount = coreEntities.Set<Identity>().Count();
                iquery = coreEntities.Set<Identity>().OrderBy(u => u.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                totalCount = coreEntities.Set<Identity>().Where(u => u.Name.Contains(selectInfo)).Count();
                iquery = coreEntities.Set<Identity>().Where(u => u.Name.Contains(selectInfo)).OrderBy(u => u.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            List<IdentityQutput> list = (from a in iquery
                                     select new IdentityQutput
                                     {
                                         ID = a.ID,
                                         EntryTime = a.EntryTime,
                                         Status = a.Status,
                                         LockTime = a.LockTime,
                                         Description = a.Description,                                        
                                         Name = a.Name,
                                     }).ToList();

            return list;
        }
    }
}
