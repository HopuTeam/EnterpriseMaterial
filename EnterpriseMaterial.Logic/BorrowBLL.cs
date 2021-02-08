using EnterpriseMaterial.Data;
using EnterpriseMaterial.Dto.BorrowDto;
using EnterpriseMaterial.ILogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseMaterial.Logic
{
    public class BorrowBLL : IBorrowBLL
    {
        private readonly CoreEntities db;

        public BorrowBLL(Data.CoreEntities _db)
        {
            db = _db;
        }
        #region 设备借取类  
        /// <summary>
        /// 查询属于设备类的全部商品
        /// </summary>
        /// <returns></returns>
        public List<Model.Goods> GetGoodsOne(out int conut, int pageinde, int pageSize)
        {
            var list = db.Goods.Where(y => y.Status == true).OrderBy(a => a.ID).Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
            conut = db.Goods.Where(x => x.Status == true).ToList().Count();
            return list;
        }
        /// <summary>
        /// 根据商品名字模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Model.Goods> GoodsOne(string name)
        {
            return db.Goods.Where(a => a.Name.Contains(name)).ToList();
        }
        /// <summary>
        /// 根据id查询物品
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Model.Goods SelectIdGoods(int ID)
        {
            return db.Goods.FirstOrDefault(a => a.ID == ID);
        }
        /// <summary>
        /// 申请领取物品
        /// 数量或者价格太多的话需要领导审批
        /// </summary>
        /// <param name="id">物品id</param>
        /// <param name="number">申请的数量</param>
        /// <param name="description">申请理由</param>
        /// <returns></returns>
        public bool ToapplyOne(int GoodsID, int Number, string Description, int Uid)
        {
            var mod = db.Goods.FirstOrDefault(x => x.ID == GoodsID);
            if (mod.Number <= 0)
                return false;
            if (mod.Number < Number)
                return false;
            bool x = true;
            //查询这个物品总价属于什么类型
            var goods = (from go in db.Goods
                         where go.ID == GoodsID
                         select go
                       ).FirstOrDefault();
            //如果是设备借取类的就要归还
            if (goods.TypeID == 2)
            {
                x = false;
            }
            //物品总价
            decimal price = goods.Money * Number;
            Model.Borrow borrow = new Model.Borrow()
            {
                GoodsID = mod.ID,
                Number = Number,
                Complete = x,
                Description = Description,
                UserID = Uid,
                SendTime = DateTime.Now,
                StatusID = 4,
            };

            db.Borrows.Add(borrow);
            if (db.SaveChanges() > 0)
            {
                goods.Number -= Number;
                db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool Agree(Dto.BorrowDto.BorrowOut borrowOut)
        {

            var mod = db.Borrows.FirstOrDefault(x => x.ID == borrowOut.ID);
            var Sta = db.BorrowStatuses.FirstOrDefault(x => x.Name == borrowOut.StatusName);
            if (mod.MiddleTime == null)
            {

                mod.Suggest = borrowOut.Suggest;
                var goog = db.Goods.FirstOrDefault(x => x.ID == mod.GoodsID);
                if (goog.Money * borrowOut.Number > 100)
                {
                    mod.StatusID = 3;
                    mod.MiddleTime = DateTime.Now;
                    if (db.SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
                mod.StatusID = 2;
                mod.EndTime = DateTime.Now;
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;

            }
            else
            {
                mod.StatusID = 2;
                mod.EndTime = DateTime.Now;
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }

        }
        #endregion
        #region 耗材申领  
        public List<Model.Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize)
        {
            var list = db.Goods.Where(a => a.TypeID == 2).OrderBy(a => a.ID).Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
            conut = list.Count();
            return list;
        }
        public List<Model.Goods> GoodsTwo(string name)
        {
            return db.Goods.Where(a => a.Name.Contains(name) && a.TypeID == 2).ToList();
        }




        #endregion
        #region 查询申请
        //查询申请表
        public string UpBorrow(out int conut, int pageinde, int pageSize)
        {
            var mod = (from Borrows in db.Borrows
                       join Users in db.Users on Borrows.UserID equals Users.ID
                       join Goods in db.Goods on Borrows.GoodsID equals Goods.ID
                       join Statuses in db.BorrowStatuses on Borrows.StatusID equals Statuses.ID
                       where Borrows.SendTime != null && Borrows.MiddleTime == null && Borrows.EndTime == null && Statuses.ID != 5
                       select new
                       {
                           ID = Borrows.ID,

                           GoodsName = Goods.Name,
                           UserName = Users.Name,
                           Description = Borrows.Description,
                           StatusName = Statuses.Name,
                           SendTime = Borrows.SendTime,
                           MiddleTime = Borrows.MiddleTime,
                           EndTime = Borrows.EndTime,
                           Number = Borrows.Number,
                           Complete = Borrows.Complete,

                       }).ToList();
            conut = mod.Count();
            var list = mod.Skip((pageinde - 1) * pageSize).Take(pageSize);
            return JsonConvert.SerializeObject(list);
        }

        #endregion
        //上级领导查询申请
        public string UpSuperior(out int conut, int pageinde, int pageSize)
        {
            var mod = (from Borrows in db.Borrows
                       join Users in db.Users on Borrows.UserID equals Users.ID
                       join Goods in db.Goods on Borrows.GoodsID equals Goods.ID
                       join Statuses in db.BorrowStatuses on Borrows.StatusID equals Statuses.ID
                       where Borrows.MiddleTime != null && Borrows.EndTime == null && Statuses.ID != 5
                       select new
                       {
                           ID = Borrows.ID,
                           GoodsName = Goods.Name,
                           UserName = Users.Name,
                           Description = Borrows.Description,
                           StatusName = Statuses.Name,
                           Suggest = Borrows.Suggest,
                           SendTime = Borrows.SendTime,
                           MiddleTime = Borrows.MiddleTime,
                           EndTime = Borrows.EndTime,
                           Number = Borrows.Number,

                       }).ToList();          
            conut = mod.Count();
            var list = mod.Skip((pageinde - 1) * pageSize).Take(pageSize);
            return JsonConvert.SerializeObject(list);
        }

        public BorrowOut Upapply(int Bid)
        {
            var mod = (from Borrows in db.Borrows
                       join Users in db.Users on Borrows.UserID equals Users.ID
                       join Goods in db.Goods on Borrows.GoodsID equals Goods.ID
                       join Statuses in db.BorrowStatuses on Borrows.StatusID equals Statuses.ID
                       where Borrows.ID == Bid
                       select new
                       {
                           ID = Borrows.ID,
                           GoodsName = Goods.Name,
                           UserName = Users.Name,
                           Gid = Borrows.GoodsID,
                           Uid = Borrows.UserID,
                           Description = Borrows.Description,
                           StatusName = Statuses.Name,
                           Suggest = Borrows.Suggest,
                           Number = Borrows.Number,
                           Complete = Borrows.Complete
                       }).FirstOrDefault();
            string Complete = null;
            if (mod.Complete)
            {
                Complete = "耗材领用";
            }
            else
            {
                Complete = "设备借取";
            }
            BorrowOut borrow = new BorrowOut()
            {
                ID = mod.ID,
                GoodsName = mod.GoodsName,
                UserName = mod.UserName,
                Goodsid = -mod.Gid,
                Userid = mod.Uid,
                Description = mod.Description,
                StatusName = mod.StatusName,
                Suggest = mod.Suggest,
                Number = mod.Number,
                Complete = Complete
            };

            return borrow;
        }



        public String Userapply(int Uid)
        {
            var mod = (from Borrows in db.Borrows
                       join Users in db.Users on Borrows.UserID equals Users.ID
                       join Goods in db.Goods on Borrows.GoodsID equals Goods.ID
                       join Statuses in db.BorrowStatuses on Borrows.StatusID equals Statuses.ID
                       where Borrows.UserID == Uid && Borrows.EndTime != null
                       select new
                       {
                           ID = Borrows.ID,
                           GoodsName = Goods.Name,
                           UserName = Users.Name,
                           Gid = Borrows.GoodsID,
                           Uid = Borrows.UserID,
                           Description = Borrows.Description,
                           StatusName = Statuses.Name,
                           Suggest = Borrows.Suggest,
                           Number = Borrows.Number,
                           Complete = Borrows.Complete
                       }).ToList();
            return JsonConvert.SerializeObject(mod);
        }



        #region 用户领取管理
        public string Downpass(out int conut, int Uid, int pageinde, int pageSize)
        {
            var mod = (from Borrows in db.Borrows
                       join Users in db.Users on Borrows.UserID equals Users.ID
                       join Goods in db.Goods on Borrows.GoodsID equals Goods.ID
                       join Statuses in db.BorrowStatuses on Borrows.StatusID equals Statuses.ID
                       where Statuses.ID == 2 && Users.ID == Uid
                       select new
                       {
                           ID = Borrows.ID,
                           GoodsName = Goods.Name,
                           UserName = Users.Name,
                           Description = Borrows.Description,
                           StatusName = Statuses.Name,
                           Suggest = Borrows.Suggest,
                           SendTime = Borrows.SendTime,
                           MiddleTime = Borrows.MiddleTime,
                           EndTime = Borrows.EndTime,
                           Number = Borrows.Number,

                       }).Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
            conut = mod.Count();
            var list = JsonConvert.SerializeObject(mod);
            return list;

        }
        //申请管理
        public string Getapply(out int conut, int Uid, int pageinde, int pageSize)
        {
            var mod = (from Borrows in db.Borrows
                       join Users in db.Users on Borrows.UserID equals Users.ID
                       join Goods in db.Goods on Borrows.GoodsID equals Goods.ID
                       join Statuses in db.BorrowStatuses on Borrows.StatusID equals Statuses.ID
                       where Statuses.ID == 2 && Users.ID == Uid
                       select new
                       {
                           ID = Borrows.ID,
                           GoodsName = Goods.Name,
                           UserName = Users.Name,
                           Description = Borrows.Description,
                           StatusName = Statuses.Name,
                           Suggest = Borrows.Suggest,
                           SendTime = Borrows.SendTime,
                           MiddleTime = Borrows.MiddleTime,
                           EndTime = Borrows.EndTime,
                           Number = Borrows.Number,

                       }).ToList();
            conut = mod.Count();
            var list = mod.Skip((pageinde - 1) * pageSize).Take(pageSize);
            return JsonConvert.SerializeObject(list);
        }
        #endregion
    }
}
