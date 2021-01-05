using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseMaterial.ILogic
{
    //public interface IBorrowBLL
    //{
    //    #region 设备借取  
    //    List<Model.Goods> GetGoodsOne(out int conut, int pageinde, int pageSize);
    //    List<Model.Goods> GoodsOne(string name);
    //    Model.Goods SelectIdGoods(int ID);
    //    bool ToapplyOne(int UserID, int Number, string Description, int GoodsID);
    //  #endregion

    //    #region 耗材申领
    //    List<Model.Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize);
    //    List<Model.Goods> GoodsTwo(string name);
    //    #endregion

<<<<<<< .mine
    //    #region 部门管理申请
||||||| .r31
        #region 部门管理申请
=======
        #region 查询申请
>>>>>>> .r35

<<<<<<< .mine
    //    //管理员查询申请
    //    List<Model.Borrow> UpBorrow(out int conut, int pageinde, int pageSize);
    //    //上级领导查询申请
    //    List<Model.Borrow> UpSuperior(out int conut, int pageinde, int pageSize);
    //    //同意申请
    //    bool Agree();
    //    //不同意
    //    bool Disagress();
    //    //删 
    //    bool DeleteBrw();
||||||| .r31
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
=======
        //查询申请
        List<Model.Borrow> UpBorrow();
        //查询名字
        public List<Model.User> UpUsers();
        //查询物品名字
        public List<Model.Goods> UpGood();
        //查询状态
        public List<Model.BorrowStatus> UpStatu();

  #endregion

        //上级领导查询申请
        List<Model.Borrow> UpSuperior(out int conut, int pageinde, int pageSize);
        //同意申请
       // bool Agree();
        //不同意
      //  bool Disagress();
        //删 
     //   bool DeleteBrw();
>>>>>>> .r35

<<<<<<< .mine
    //    //该
    //    bool EidtBrw();
    //    //模糊查询申请
||||||| .r31
        //该
        bool EidtBrw();
        //模糊查询申请
=======
        //该
       //bool EidtBrw();
        //模糊查询申请
>>>>>>> .r35

<<<<<<< .mine
    //    #endregion
||||||| .r31
        #endregion
=======
      
>>>>>>> .r35


    //}
}
