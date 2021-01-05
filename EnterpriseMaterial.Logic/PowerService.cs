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
                string powerid = powerEntity.Description;
                //【2.2】查询该用户拥有的所有角色的角色权限中间表--(可以使用缓存优化)
                var list = _dbContext.Set<Power>().Where(u => allRoleId.Contains(u.RoleId)).ToList();
                if (list.Count > 0)
                {
                    //【2.3】查询角色权限表，看看该是否拥有此powerid权限
                    int count = list.Where(u => u.PowerId == powerid).Count();
                    if (count > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public List<PowerOutput> GetMenu(string roleID)
        {
            throw new NotImplementedException();
        }

        public PowerTreeOutput GetPowerList(string allRoleID)
        {
            throw new NotImplementedException();
        }

        public bool SetPower(string strId, string roleId)
        {
            throw new NotImplementedException();
        }
    }
}
