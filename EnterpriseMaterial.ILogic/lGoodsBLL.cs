using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
   public interface lGoodsBLL
    {
        #region 设备借取  
        List<Model.Goods> GetGoodsOne(out int  conut,int pageinde,int pageSize);
        List<Model.Goods> GoodsOne(string name);
        Model.Goods SelectIdGoods(int ID);
        bool ToapplyOne(int id, int number,string description);
        #endregion

        #region 耗材申领
        List<Model.Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize);
        List<Model.Goods> GoodsTwo(string name);
        #endregion
    }
}
