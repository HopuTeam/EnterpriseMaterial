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
using EnterpriseMaterial.Common;

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
                Name = inputEntity.Name,
            };

            coreEntities.Set<Identity>().Add(entity);
            return coreEntities.SaveChanges();
        }



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public int Update(identityInput inputEntity)
        {
            Identity entity = coreEntities.Set<Identity>().Where(u => u.ID == inputEntity.RoleId).FirstOrDefault();
            if (entity != null)
            {
                entity.Description = inputEntity.Description;
                entity.Name = inputEntity.Name;
                coreEntities.Set<Identity>().Attach(entity);
                coreEntities.Set<Identity>().Update(entity);
                return coreEntities.SaveChanges();
            }
            else
            {
                return 0;
            }

        }


        /// <summary>
        /// 普通查询,获取未被禁用的所有
        /// </summary>
        /// <returns></returns>
        public List<IdentityQutput> LoadEntities()// 普通查询,获取未被禁用的所有
        {

            List<IdentityQutput> list = (from a in coreEntities.Set<Identity>().Where(u => u.Status==false)
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
        /// 开启/禁用角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool ForbidRole(int id, string flag)
        {

            Identity entity = coreEntities.Set<Identity>().Where(u => u.ID == id).FirstOrDefault();
            if (entity != null)
            {
                if (flag == "true")
                {
                    entity.Status = true;//表示开启
                    coreEntities.Set<Identity>().Attach(entity);
                    coreEntities.Set<Identity>().Update(entity);
                    if (coreEntities.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                else
                {
                    entity.Status = false;//表示禁用
                    coreEntities.Set<Identity>().Attach(entity);
                    coreEntities.Set<Identity>().Update(entity);
                    if (coreEntities.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
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


        /// <summary>
        /// 判断RoleID是否重复,重复返回true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckRepeat(int roleID)
        {
            if (coreEntities.Set<Identity>().Where(u => u.ID == roleID).Count() > 0)

            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public LayuiTreeModel LayuiTreeModels(int uid)
        {
            throw new NotImplementedException();
        }






        #endregion




    }
}
