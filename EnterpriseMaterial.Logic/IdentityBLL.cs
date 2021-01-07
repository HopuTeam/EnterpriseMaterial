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
        
        
        #region 构造函数注入
        private readonly ILogger<IdentityBLL> logger;
        private readonly CoreEntities coreEntities;
       


        public IdentityBLL(ILogger<IdentityBLL> logger,CoreEntities coreEntities)//
        {
            this.logger = logger;
            this.coreEntities = coreEntities;
        }


        #endregion






        #region MyRegion

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        //public int Add(RoleInput inputEntity)
        //{

        //    RoleInfo entity = new RoleInfo
        //    {
        //        Id = inputEntity.Id,
        //        AddTime = DateTime.Now,
        //        DelFlag = 0,
        //        DelTime = DateTime.Now,
        //        Description = inputEntity.Description,
        //        RoleId = inputEntity.RoleId,
        //        RoleName = inputEntity.RoleName,
        //    };

        //    _dbContext.Set<RoleInfo>().Add(entity);
        //    return _dbContext.SaveChanges();
        //}





        /// <summary>
        /// 普通查询,获取未被禁用的所有
        /// </summary>
        /// <returns></returns>
        public List<IdentityQutput> LoadEntities()// 普通查询,获取未被禁用的所有
        {

            List<IdentityQutput> list = (from a in coreEntities.Set<Identity>().Where(u => u.Status)
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

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <returns></returns>
        public List<IdentityQutput> LoadEntities(int id)// 根据id查询
        {
            List<IdentityQutput> list = (from a in coreEntities.Set<Identity>().Where(u => u.ID == id)
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







        ///实现接口 并在这里处理数据    
        public List<IdentityQutput> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo)//分页查询
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









        #endregion




    }
}
