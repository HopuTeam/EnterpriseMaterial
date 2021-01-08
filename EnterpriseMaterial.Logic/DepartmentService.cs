﻿using EnterpriseMaterial.Data;
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
            return (from a in _dbContext.Set<Department>()
                    join b in _dbContext.Set<User>() on a.UserID equals b.ID into join_a
                    from c in join_a.DefaultIfEmpty()
                    select new DepartmentOutput
                    {
                       
                        Name = a.Name,
                        Id = a.ID,
                        UserID = a.UserID,
                        ParentID = a.ParentID,
                        LeaderName = c.Name,

                    }).ToList();
        }

        public List<DepartmentParentOutput> GetReviewerMsg(int userId)
        {
            //查到对应用户信息
            User userEntity = _dbContext.Set<User>().Where(u => u.ID == userId).FirstOrDefault();
            //查到对应用户的部门信息--只有一条记录（暂时只做一对一的关系，一个人只有一个部门，一个部门只有一个领导）
            List<Department> dpEntity = _dbContext.Set<Department>().Where(u => u.ID == userEntity.DepartmentID).ToList();
            List<DepartmentParentOutput> list = new List<DepartmentParentOutput>();
            GetParentBoss(ref list, dpEntity);
            return list;
        }
        public List<DepartmentOutput> LoadEntities(int id)
        {
            throw new NotImplementedException();
        }

        public object LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo)
        {
            
            if (selectInfo==null)
            {
                var list = (from deq in _dbContext.Departments
                            join u in _dbContext.Users on deq.UserID equals u.SignID
                            select new
                            {
                                ID = deq.ID,
                                DeqName = deq.Name,
                                Uname = u.Name,
                                VipName = (from d in _dbContext.Departments
                                           where d.ID == deq.ParentID
                                           select d.Name
                                      ).FirstOrDefault(),
                                ShiJian = deq.EntryTime
                            }
                   ).OrderBy(u => u.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                totalCount = list.Count();
                return list;
            }
            else
            {
                IQueryable<Department> iquery = _dbContext.Set<Department>().Where(u => u.Name.Contains(selectInfo)).OrderBy(u => u.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize) ;
                var list = (from a in iquery
                            join u in _dbContext.Users on a.UserID equals u.ID into join_a
                            from c in join_a.DefaultIfEmpty()
                            join d in _dbContext.Departments on a.ParentID equals d.ID
                            select new
                            {
                                ID = a.ID,
                                DeqName = a.Name,
                                Uname = c.Name,
                                VipName = d.Name,
                                ShiJian = a.EntryTime
                            }
                          ).ToList();
               

                  totalCount = list.Count();
                return list;
            }
         
        }

        public int Update(DepartmentInput inputEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
        public void GetParentBoss(ref List<DepartmentParentOutput> list, List<Department> sonEntity)
        {
            DepartmentParentOutput parenEntity = new DepartmentParentOutput();

            //连表->获取本级别部门领导名字,并存起来
            parenEntity = (from a in sonEntity
                           join b in _dbContext.Set<User>() on a.UserID equals b.ID into join_a
                           from c in join_a.DefaultIfEmpty()
                           select new DepartmentParentOutput
                           {
                               LeaderId = a.UserID,
                               LeaderName = c.Name,
                           }).FirstOrDefault();
            list.Add(parenEntity);
            if (sonEntity[0].ParentID == sonEntity[0].ID)
            {
                //说明到最顶级
                return;
            }
            else
            {
                //查到他的上一级部门
                List<Department> parentList = _dbContext.Set<Department>().Where(u => u.ID == sonEntity[0].ParentID).ToList();
                GetParentBoss(ref list, parentList);
            }
        }
    }
}