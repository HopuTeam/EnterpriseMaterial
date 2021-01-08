using EnterpriseMaterial.Model;
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
        #endregion
        bool ToapplyOne(int GoodsID, int Number, string Description, int Uid);
        #region 耗材申领
        List<Model.Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize);
        List<Model.Goods> GoodsTwo(string name);
        //管理员查询申请
        string UpBorrow(out int conut, int pageinde, int pageSize);
        //上级领导查询申请
        string UpSuperior(out int conut, int pageinde, int pageSize);
        //同意申请
        Dto.BorrowDto.BorrowOut Upapply(int Bid);
        bool Agree(Dto.BorrowDto.BorrowOut borrow);
        String Userapply(int Uid);
        #endregion


        #region 用户领取
     string Downpass( out int conut,int Uid, int pageinde, int pageSize);

        public string Getapply(out int conut, int Uid, int pageinde, int pageSize);
        #endregion
    }
}
