using System.Collections.Generic;

namespace EnterpriseMaterial.ILogic
{
    public interface lGoodsBLL
    {
        #region 设备借取  
        List<Model.Goods> GetGoodsOne(out int conut, int pageinde, int pageSize);
        List<Model.Goods> GoodsOne(string name, out int conut, int pageinde, int pageSize);
        Model.Goods SelectIdGoods(int ID);
        bool ToapplyOne(int id, int number, string description);
        #endregion

        #region 耗材申领
        List<Model.Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize);
        List<Model.Goods> GoodsTwo(string name);
        #endregion

        #region 增删改查

        List<Model.Category> GetCategories();
        bool EditGoods(Model.Goods view);

        bool AddGoods(Model.Goods view);
        bool AddExcel(List<Model.Goods> goods, int Uid);
        #endregion
    }
}
