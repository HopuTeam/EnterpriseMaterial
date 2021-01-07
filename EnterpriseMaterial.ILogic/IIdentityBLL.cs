using System;
using System.Collections.Generic;
using System.Text;
using EnterpriseMaterial.Dto.IdentityDTO;


namespace EnterpriseMaterial.ILogic
{

  public  interface IIdentityBLL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        int Add(identityInput inputEntity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        int Update(identityInput inputEntity);



        /// <summary>
        /// 判断RoleID是否重复
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CheckRepeat(int value);




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
        /// 开启/禁用角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        bool ForbidRole(int id, string flag);





        /// <summary>
        /// 获取用户拥有的角色
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        //string GetRoleID(string userID);

        /// <summary>
        /// 给用户配置角色
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="arrRoleID"></param>
        /// <returns></returns>
        //bool ConfigRole(string userID, string arrRoleID);//未完成



        /// <summary>
        /// 展示角色权限树 
        /// </summary>
        /// <returns></returns>
        Common.LayuiTreeModel LayuiTreeModels(int powerid);
        /// <summary>
        /// 根据id查身份表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Model.Identity Selectid(int id);

        /// <summary>
        /// 配置权限
        /// </summary>
        /// <param name="IdentiyiList"></param>
        /// <param name="ldentityid"></param>
        /// <returns></returns>
        bool SetPower(string IdentiyiList, int ldentityid);

        /// <summary>
        /// 重置权限，删除该身份全部权限
        /// </summary>
        /// <param name="ldentityid"></param>
        /// <returns></returns>
        bool Seset(int ldentityid);



    }
}
