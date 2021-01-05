using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
    public interface IBorrowBLL
    {
        #region 设备借取  
        List<Model.Goods> GetGoodsOne(out int conut, int pageinde, int pageSize);
        List<Model.Goods> GoodsOne(string name);
        Model.Goods SelectIdGoods(int ID);
        bool ToapplyOne(int UserID, int Number, string Description, int GoodsID);
      #endregion

        #region 耗材申领
        List<Model.Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize);
        List<Model.Goods> GoodsTwo(string name);
        #endregion

        #region 部门管理申请

        //管理员查询申请
        List<Model.Borrow> UpBorrow(out int conut, int pageinde, int pageSize);
        //上级领导查询申请
        List<Model.Borrow> UpSuperior(out int conut, int pageinde, int pageSize);
        //同意申请
        bool Agree();
        //不同意
        bool Disagress();
        //删 
        bool DeleteBrw();

        //该
        bool EidtBrw();
        //模糊查询申请

        #endregion


    }
}
