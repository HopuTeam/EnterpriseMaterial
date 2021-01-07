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
        public int Add(identityInput inputEntity)
        {

            Identity  entity = new Identity
            {
                ID = inputEntity.ID,
                EntryTime = DateTime.Now,
                Status = false,
                LockTime = DateTime.Now,
                Description = inputEntity.Description,               
                Name = inputEntity.RoleName,
            };

            coreEntities.Set<Identity>().Add(entity);
            return coreEntities.SaveChanges();
        }





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



        /// <summary>
        /// 获取用户拥有的角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string GetRoleID(int userID)//可以用的
        {
            var iquery = coreEntities.Set<IdentityPower>().Where(u => u.IdentityID == userID);
            //获取roleid-->（筛选被禁止的角色，严格来说，必须筛选）
            var roleIquery = coreEntities.Set<Identity>().Where(u => u.ID > 0);
            var r_roleIquery = (from a in iquery
                                join b in roleIquery on a.IdentityID equals b.ID into b_join
                                from c in b_join.DefaultIfEmpty()
                                select new
                                {
                                    UserID = a.ID,
                                    RoleID = a.IdentityID,
                                    Status = c.Status,
                                }).Where(u => u.Status == false);//筛选
                                                              //构造角色id字符串             
            if (r_roleIquery.Count() > 0)
            {
                string strRoleID = "";
                foreach (var item in r_roleIquery)
                {
                    strRoleID += item.RoleID + ",";
                }
                //去掉最后一个逗号               
                return strRoleID.Substring(0, strRoleID.Length - 1);
            }
            else
            {
                return "0";
            }
        }









        #endregion




    }
}
