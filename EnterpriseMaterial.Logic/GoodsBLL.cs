using EnterpriseMaterial.Data;
using EnterpriseMaterial.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EnterpriseMaterial.Logic
{
    public class GoodsBLL : ILogic.lGoodsBLL
    {
        private readonly CoreEntities db;

        public GoodsBLL(Data.CoreEntities _db)
        {
            db = _db;
        }
        #region 设备借取类  
        /// <summary>
        /// 查询属于设备类的全部商品
        /// </summary>
        /// <returns></returns>
        public List<Goods> GetGoodsOne(out int conut, int pageinde, int pageSize)
        {
            var list = db.Goods.Where(y=>y.Status==true).OrderBy(a => a.ID).Skip((pageinde-1)*pageSize).Take(pageSize).ToList();
            conut = list.Count();
            return list;
        }
        /// <summary>
        /// 根据商品名字模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Goods> GoodsOne(string name, out int conut, int pageinde , int pageSize)
        {
            List<Goods> res= db.Goods.Where(a => a.Name.Contains(name)).OrderBy(c => c.ID).Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
            conut = res.Count();
            return res;
        }
        /// <summary>
        /// 根据id查询物品
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Goods SelectIdGoods(int ID)
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
        public bool ToapplyOne(int id,int number ,string description)
        {
            bool x = true;
            //查询这个物品总价属于什么类型
            var goods = (from go in db.Goods
                         where go.ID == id
                         select go
                       ).FirstOrDefault();
            //如果是设备借取类的就要归还
            if (goods.TypeID==2)
            {
                x = false;
            }
            //物品总价
            decimal price = goods.Money * number;
            Borrow borrow = new Borrow()
            {
                UserID = 1,
                Number = number,
                Complete = x,
                Description = description,
                GoodsID = id,
                SendTime = DateTime.Now,
                StatusID = 1,
            };
            //数量或者价格太多的话需要领导审批
            if (number >=10 || price >=1000)
            {
                borrow.DepartmentID = 1;
            }
            db.Borrows.Add(borrow);
            if (db.SaveChanges() > 0)
            {
                goods.Number -= number;
                db.SaveChanges();
                return true;
            }          
          else
            return false;
        }
        #endregion
        #region 耗材申领  
        public List<Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize)
        {
          var list= db.Goods.Where(a => a.TypeID == 2).OrderBy(a => a.ID).Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
            conut = list.Count();
            return list;
        }
        public List<Goods> GoodsTwo(string name)
        {
            return db.Goods.Where(a => a.Name.Contains(name) && a.TypeID == 2).ToList();
        }






        #endregion


        #region 增删改查
        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        /// <summary>
        /// 物品表的修改
        /// </summary>
        /// <param name="view">页面传进来的数据</param>
        /// <returns></returns>
        public bool EditGoods(Goods view)
        {
            Goods goods = db.Goods.FirstOrDefault(a => a.ID == view.ID);
            if (goods==null)
            {
                return false;
            }
            goods.CategoryID = view.CategoryID;
            goods.Description = view.Description;         
            goods.Money = view.Money;
            goods.Name = view.Name;
            goods.Number = view.Number;
            goods.Specification = view.Specification;
            goods.Status = view.Status;    
            goods.Unit = view.Unit;
            goods.WarningNum = view.WarningNum;
            goods.Status = view.Status;
            if (db.SaveChanges()>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public bool AddGoods(Goods view)
        {
            Goods goods = new Goods()
            {
                CategoryID = view.CategoryID,
                Description = view.Description,
                EntryTime = DateTime.Now,
                Money = view.Money,
                Name = view.Name,
                Number = view.Number,
                Specification = view.Specification,
                Status = true,
                TypeID = view.TypeID,
                Unit = view.Unit,
                WarningNum = view.WarningNum
            };
            db.Goods.Add(goods);
            if (db.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        #endregion
    }
}
