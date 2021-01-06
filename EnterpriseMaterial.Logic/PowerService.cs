using EnterpriseMaterial.Dto.PowerDTO;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnterpriseMaterial.Logic
{
    public class PowerService : IPowerService
    {
        private readonly ILogger<PowerService> _logger;
        private readonly DbContext _dbContext;
        private readonly IMemoryCache _memoryCache;//缓存
        public PowerService(ILogger<PowerService> logger, DbContext dbcontext, IMemoryCache memoryCache)
        {
            _logger = logger;
            _dbContext = dbcontext;
            _memoryCache = memoryCache;
        }
        public bool CheckPower(string url, string allRoleId)
        {
            bool result = false;
            //【1】获取对应权限信息--(可以使用缓存优化)

            List<Power> powerInfoList = _memoryCache.Get<List<Power>>("powerInfoList");
            if (powerInfoList == null)
            {
                var plist = _dbContext.Set<Power>().Where(u => u.ID > 0).ToList();
                _memoryCache.Set<List<Power>>("powerInfoList", plist, new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddMinutes(100) });//设置绝对过期时间100分钟，(因为系统没有修改数据库的功能，手动直接修改数据库无法清空缓存,信息滞后100min，解决办法可以在页面给个情况缓存按钮)
                powerInfoList = plist;
            }
            Power powerEntity = _dbContext.Set<Power>().Where(u => u.ActionUrl == url).FirstOrDefault();
            if (powerEntity != null)
            {
                int powerid = powerEntity.ParentID;
                //【2.2】查询该用户拥有的所有角色的角色权限中间表--(可以使用缓存优化)
                var list = _dbContext.Set<IdentityPower>().Where(u => allRoleId.Contains((char)u.IdentityID)).ToList();
                if (list.Count > 0)
                {
                    //【2.3】查询角色权限表，看看该是否拥有此powerid权限
                    int count = list.Where(u => u.PowerID == powerid).Count();
                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public List<PowerOutput> GetMenu(string allRoleID)
        {
            //【1】拿到对应用户的所有权限,和权限表的所有信息--(从缓存中拿数据)                
            List<IdentityPower> rRoleInfoPowerInfoList = _memoryCache.Get<List<IdentityPower>>("rRoleInfoPowerInfoList");

            if (rRoleInfoPowerInfoList == null)
            {


                //如果缓存中数据没有，再去数据库查，并放入缓存
                var rlist = _dbContext.Set<IdentityPower>().Where(u => u.ID > 0).ToList();

                _memoryCache.Set<List<IdentityPower>>("rRoleInfoPowerInfoList", rlist, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(100) });//设置划滑动过期时间100分钟,注意：当修改数据库时必须清空缓存

                rRoleInfoPowerInfoList = rlist;
            }
            List<Power> powerInfoList = _memoryCache.Get<List<Power>>("powerInfoList");
            if (powerInfoList == null)
            {
                //如果缓存中数据没有，再去数据库查，并放入缓存
                var plist = _dbContext.Set<Power>().Where(u => u.ID > 0).ToList();
                _memoryCache.Set<List<Power>>("powerInfoList", plist, new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddMinutes(100) });//设置绝对过期时间100分钟，(因为系统没有修改数据库的功能，手动直接修改数据库无法清空缓存，信息滞后100min，解决办法可以在页面给个情况缓存按钮)
                powerInfoList = plist;
            }
            //【2】和权限表连表查询，并筛选出菜单权限
            var listMenu = (from a in rRoleInfoPowerInfoList.Where(u => allRoleID.Contains((char)u.IdentityID))
                            join b in powerInfoList on a.PowerID equals b.PowerId into b_join
                            from c in b_join.DefaultIfEmpty()
                            select c).Where(u => u.Description.Contains("目录")).ToList();
            ////【3】去掉重复的权限
            ////var listOKMenu = listMenu.Distinct(new PowerInfoComparer());

            ////【3.1】原始的去重思路，第一步：创建一个新的list集合
            List<Power> list = new List<Power>();
            ////第二步：遍历查询到的数据
            foreach (var item in listMenu)
            {
                //如果list没有数据，直接添加
                if (list.Count == 0)
                {
                    list.Add(item);
                }
                int flag = 0;//标记
                //第三步：遍历新的list集合
                foreach (var item1 in list)
                {
                    //第四步：如果新的list集合里，已经存在的数据，把flag改成1
                    if (item1.PowerId == item.PowerId)
                    {
                        flag = 1;
                    }
                }
                if (flag == 0)//只有当flag等于0 的时候，才会把数据添加到新的list集合中
                {
                    list.Add(item);
                }
            }
            //第二步：遍历查询到的数据
            foreach (var item in listMenu)
            {
                //如果list没有数据，直接添加
                if (list.Count == 0)
                {
                    list.Add(item);
                }
                int flag = 0;//标记
                             //第三步：遍历新的list集合
                foreach (var item1 in list)
                {
                    //第四步：如果新的list集合里，已经存在的数据，把flag改成1
                    if (item1.PowerId == item.PowerId)
                    {
                        flag = 1;
                    }
                }
                if (flag == 0)//只有当flag等于0 的时候，才会把数据添加到新的list集合中
                {
                    list.Add(item);
                }
            }
            //【4】转换
            List<PowerOutput> outlist = (from a in list
                                         select new PowerOutput
                                         {
                                             ID = a.ID,
                                             ActionUrl = a.ActionUrl,
                                             Description = a.Description,
                                             MenuIcon = a.MenuIcon,
                                             Name = a.Name,
                                             ParentID = a.ParentID,
                                             PowerId = a.PowerId,
                                         }).ToList();
            return outlist;
        }
        public PowerTreeOutput GetPowerList(int roleId)
        {
            //获取顶级目录,并连表
            List<Power> d_list = _dbContext.Set<Power>().Where(u => u.ParentID == 0).ToList();

            PowerTreeOutput powerTreeEntity = new PowerTreeOutput()
            {
                Id = "0",
                Spread = true,
                Title = "权限目录",
                Field = "",
                Disabled = false,
                Checked = false,//这个设置为true，则默认子节点全部为true
                Href = "",
            };
            DiGui(ref powerTreeEntity, d_list, roleId);

            return powerTreeEntity;
        }

        public bool SetPower(string strId, int roleId)
        {
            //【0】不理那么多，先清空缓存
            _memoryCache.Remove("rRoleInfoPowerInfoList");
            //【1】先查是否有数据,如果有就数据先删除
            List<IdentityPower> rlist = _dbContext.Set<IdentityPower>().Where(u => u.IdentityID == roleId).ToList();

            if (rlist.Count > 0)
            {

                _dbContext.Set<IdentityPower>().AttachRange(rlist);
                _dbContext.Set<IdentityPower>().RemoveRange(rlist);

                if (_dbContext.SaveChanges() > 0)
                {
                    //【2】添加数据
                    string[] arrStr = strId.Split();
                    for (int i = 0; i < arrStr.Length - 1; i++)
                    {

                        IdentityPower entity = new IdentityPower
                        {

                            PowerID = Convert.ToInt32(arrStr[i]),
                            IdentityID = roleId,
                        };
                        _dbContext.Set<IdentityPower>().Add(entity);
                    }
                    if (_dbContext.SaveChanges() > 0)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //【2】添加数据
                string[] arrStr = strId.Split();
                for (int i = 0; i < arrStr.Length - 1; i++)
                {

                    IdentityPower entity = new IdentityPower
                    {

                        PowerID = Convert.ToInt32(arrStr[i]),
                        IdentityID = roleId,
                    };
                    _dbContext.Set<IdentityPower>().Add(entity);

                }
                if (_dbContext.SaveChanges() > 0)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        /// <summary>
        /// 递归构造目录树结构
        /// </summary>
        /// <param name="powerTreeEntity"></param>
        /// <param name="sonlist"></param>
        private void DiGui(ref PowerTreeOutput powerTreeEntity, List<Power> sonlist, int roleId)
        {

            List<PowerTreeOutput> p_list = new List<PowerTreeOutput>();
            foreach (var item in sonlist)
            {

                //查看对应角色是否拥有该权限
                IdentityPower rEntity = _dbContext.Set<IdentityPower>().AsNoTracking().Where(u => u.IdentityID == roleId && u.PowerID == item.PowerId).FirstOrDefault();

                //赋值
                PowerTreeOutput entity = new PowerTreeOutput()
                {
                    Id = Convert.ToString(item.PowerId),
                    Spread = rEntity != null ? true : false,
                    Title = item.Name + "--->" + item.ActionUrl,
                    Field = "",
                    Disabled = false,
                    Checked = rEntity != null ? true : false,
                    Href = "",
                };


                //查询是否有子目录
                List<Power> d_list = _dbContext.Set<Power>().Where(u => u.ParentID == item.PowerId).ToList();
                if (d_list.Count > 0)
                {
                    entity.Checked = false;//如果有子节点,设置为false
                    DiGui(ref entity, d_list, roleId);
                }

                p_list.Add(entity);
                powerTreeEntity.Children = p_list;
            }
        }
    }
}
