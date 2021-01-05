using EnterpriseMaterial.Dto.PowerDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
   public interface IPowerService
    {
        PowerTreeOutput GetPowerList(string allRoleID);

        /// <summary>
        /// 给角色配置权限
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        bool SetPower(string strId, string roleId);

        /// <summary>
        /// 判断用户是否有权限--待缓存优化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool CheckPower(string url, string allRoleId);

        /// <summary>
        /// 根据角色ID字符串获取对应的目录权限--缓存优化
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        List<PowerOutput> GetMenu(string roleID);
    }
}
