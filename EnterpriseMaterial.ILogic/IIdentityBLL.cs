using System;
using System.Collections.Generic;
using System.Text;
using EnterpriseMaterial.Dto.IdentityDTO;


namespace EnterpriseMaterial.ILogic
{

  public  interface IIdentityBLL
    {









        #region 查询

       
        /// <summary>
        /// 普通查询
        /// </summary>
        /// <returns></returns>
        List<IdentityQutput> LoadEntities();

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <returns></returns>
        List<IdentityQutput> LoadEntities(int id);





        /// <summary>//调用接口
        /// 分页查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        List<IdentityQutput> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo);


        #endregion



        /// <summary>
        /// 获取用户拥有的角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        string GetRoleID(string userID);

        /// <summary>
        /// 给用户配置角色
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="arrRoleID"></param>
        /// <returns></returns>
        //bool ConfigRole(string userID, string arrRoleID);//未完成













    }
}
