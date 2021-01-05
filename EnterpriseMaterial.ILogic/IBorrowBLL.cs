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
        bool ToapplyOne(int id, int number, string description);
      #endregion

        #region 耗材申领
        List<Model.Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize);
        List<Model.Goods> GoodsTwo(string name);
        #endregion



        #region 用户物料申请
        bool Verify(Model.Borrow borrow);



        #endregion
    }
}
