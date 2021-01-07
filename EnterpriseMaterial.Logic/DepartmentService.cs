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
            User userEntity = _dbContext.Set<User>().Where(u => u.ID == userId).FirstOrDefault();
            //查到对应用户的部门信息--只有一条记录（暂时只做一对一的关系，一个人只有一个部门，一个部门只有一个领导）
            List<Departments> dpEntity = _dbContext.Set<Departments>().Where(u => u.ID == userEntity.DepartmentID).ToList();
            List<DepartmentParentOutput> list = new List<DepartmentParentOutput>();
            GetParentBoss(ref list, dpEntity);
            return list;
        }

        public List<DepartmentOutput> LoadEntities(int id)
        {
            throw new NotImplementedException();
        }

        public List<DepartmentOutput> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo)
        {
            IQueryable<Departments> iquery;
            if (string.IsNullOrEmpty(selectInfo))
            {
                totalCount = _dbContext.Set<Departments>().Count();
                iquery = _dbContext.Set<Departments>().OrderBy(u => u.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            }
            else
            {
                totalCount = _dbContext.Set<Departments>().Where(u => u.Name.Contains(selectInfo)).Count();
                iquery = _dbContext.Set<Departments>().Where(u => u.Name.Contains(selectInfo)).OrderBy(u => u.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            var linq = from a in iquery
                       join b in _dbContext.Set<User>() on a.UserID equals b.ID into join_a
                       from c in join_a.DefaultIfEmpty()
                       join e in _dbContext.Set<Departments>() on a.ParentID equals e.ID
                       select new DepartmentOutput
                       {
                          
                           Name = a.Name,
                           Id = a.ID,
                           UserID = a.UserID,
                           LeaderName = c.Name,
                           ParentID = a.ParentID,
                           ParentIdName = e.Name,
                           EntryTime = a.EntryTime,
                       };
            return linq.ToList();
        }

        public int Update(DepartmentInput inputEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
        public void GetParentBoss(ref List<DepartmentParentOutput> list, List<Departments> sonEntity)
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
                List<Departments> parentList = _dbContext.Set<Departments>().Where(u => u.ID == sonEntity[0].ParentID).ToList();
                GetParentBoss(ref list, parentList);
            }
        }


    }
}
