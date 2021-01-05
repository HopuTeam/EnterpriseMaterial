using System;
using System.Collections.Generic;
using System.Text;
using EnterpriseMaterial.Dto.IdentityDTO;


namespace EnterpriseMaterial.ILogic
{

  public  interface IIdentityBLL
    {
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











    }
}
