using EnterpriseMaterial.Dto.DepartmentDTO;
using System.Collections.Generic;

namespace EnterpriseMaterial.ILogic
{
    public interface IDepartmentService
    { /// <summary>
      /// 获取所有部门信息
      /// </summary>
      /// <returns></returns>
        List<DepartmentOutput> GetList();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <returns></returns>
        object LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string selectInfo);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        string AddDep(Model.Department inputEntity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        int Update(DepartmentInput inputEntity);


        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <returns></returns>
        Model.Department  SelectId(int id);

        /// <summary>
        /// 查询用户表，为了给添加修改部门做选择
        /// </summary>
        /// <returns></returns>
        List<Model.User> SelectUser();

        /// <summary>
        /// 获取签核人的信息（对应部门的领导）--递归获取对应用户以及他所有上级领导的信息，并从上往下排列好
        /// </summary>
        /// <returns></returns>
        List<DepartmentParentOutput> GetReviewerMsg(int userId);
    }
}

