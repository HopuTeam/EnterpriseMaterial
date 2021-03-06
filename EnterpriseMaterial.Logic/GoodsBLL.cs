﻿using EnterpriseMaterial.Data;
using EnterpriseMaterial.Dto.GoodsDTO;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnterpriseMaterial.Logic
{
    public class GoodsBLL : lGoodsBLL
    {
        private readonly CoreEntities db;

        public GoodsBLL(CoreEntities _db)
        {
            db = _db;
        }

        /// <summary>
        /// 查询全部物品
        /// </summary>
        /// <returns></returns>
        public List<GoodsViewModelDTO> GetGoodsOne(out int conut, int pageinde, int pageSize)
        {
            List<Goods> mod = db.Goods.ToList();
            List<Goods> list = mod.OrderBy(a => a.ID).Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
            conut = mod.Count();         
            List<GoodsViewModelDTO> view = (from goods in list
                        join categories in db.Categories on goods.CategoryID equals categories.ID
                        join type in db.Types on goods.TypeID equals type.ID
                        select new GoodsViewModelDTO
                        {
                            ID = goods.ID,
                            Category = categories.Name,
                            Description = goods.Description,
                            EntryTime = goods.EntryTime,
                            Money = goods.Money,
                            Name = goods.Name,
                            Number = goods.Number,
                            Specification = goods.Specification,
                            Status = goods.Status,
                            Type = type.Name,
                            Unit = goods.Unit,
                            WarningNum = goods.WarningNum
                        }
                      ).ToList();
            return view;
        }
        /// <summary>
        /// 根据商品名字模糊查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Goods> GoodsOne(string name, out int conut, int pageinde, int pageSize)
        {
            List<Goods> res = db.Goods.Where(a => a.Name.Contains(name)).OrderBy(c => c.ID).Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
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
        public bool ToapplyOne(int id, int number, string description)
        {
            bool x = true;
            //查询这个物品总价属于什么类型
            var goods = (from go in db.Goods
                         where go.ID == id
                         select go
                       ).FirstOrDefault();
            //如果是设备借取类的就要归还
            if (goods.TypeID == 2)
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
            //if (number >=10 || price >=1000)
            //{
            //    borrow.DepartmentID = 1;
            //}
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

        #region 耗材申领  
        public List<Goods> GetGoodsTwo(out int conut, int pageinde, int pageSize)
        {
            var list = db.Goods.Where(a => a.TypeID == 2).OrderBy(a => a.ID).Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
            conut = list.Count();
            return list;
        }
        public List<Goods> GoodsTwo(string name)
        {
            return db.Goods.Where(a => a.Name.Contains(name) && a.TypeID == 2).ToList();
        }






        #endregion
        #region 增删改查
        public List<Model.Category> GetCategories()
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
            if (goods == null)
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
            if (db.SaveChanges() > 0)
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

        /// <summary>
        /// Excel批量导入
        /// </summary>
        /// <param name="goods"></param>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public bool AddExcel(List<Dto.GoodsDTO.ExcelTheimportModel> excels, int Uid)
        {
            foreach (var item in excels)
            {
                Goods entity = db.Goods.FirstOrDefault(a => a.Name == item.Name);
                if (entity == null)
                {
                    //添加物品
                    Goods addentity = new Goods()
                    {
                        Name = item.Name,
                        CategoryID = SelectCategoriesName(item.Category),
                        Description = item.Description,
                        EntryTime = DateTime.Now,
                        Money = item.Money,
                        Number = item.Number,
                        Specification = item.Specification,
                        Status = true,
                        TypeID = SelectTypelName(item.Type),
                        Unit = item.Unit,
                        WarningNum = item.WarningNum
                    };
                    db.Goods.Add(addentity);
                    db.SaveChanges();
                    //往入库信息表添加记录
                    Obtain obtain = new Obtain()
                    {
                        EntryTime = DateTime.Now,
                        GoodsID = addentity.ID,
                        Number = item.Number,
                        UserID = Uid
                    };
                    db.Obtains.Add(obtain);
                }
                else
                {
                    //修改物品信息
                    entity.Number += item.Number;
                    entity.Money = item.Money;
                    entity.Specification = item.Specification;
                    entity.Unit = item.Unit;
                    entity.WarningNum = item.WarningNum;
                    entity.Description = item.Description;
                    entity.TypeID = SelectTypelName(item.Type);
                    entity.CategoryID = SelectCategoriesName(item.Category);
                    db.Set<Goods>().Attach(entity);
                    db.Set<Goods>().Update(entity);
                }

            }
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据商品分类名字查询出分类id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SelectCategoriesName(string name)
        {
            return db.Categories.FirstOrDefault(a => a.Name == name).ID;
        }
        /// <summary>
        /// 根据Typel分类名字查询出分类id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SelectTypelName(string name)
        {
            return db.Types.FirstOrDefault(a => a.Name == name).ID;
        }
        #endregion
    }
}
