using EnterpriseMaterial.Common;
using EnterpriseMaterial.Data;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseMaterial.Web.Controllers
{
    public class BorrowController : Controller
    {
        public IBorrowBLL BorrowBLL { get; }

        public BorrowController(ILogic.IBorrowBLL borrowBLL)//Data.CoreEntities borrow,
        {
            BorrowBLL = borrowBLL;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region 设备借取  
        /// <summary>
        /// 设备借取页面
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexOne()
        {
            return View();
        }
        public string GetListOne(int page = 1, int limit = 5)
        {
            int dataConunt;
            List<Model.Goods> list = BorrowBLL.GetGoodsOne(out dataConunt, page, limit);
            var result = new LayuiJsonModel<Model.Goods>()
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = list
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// 申请界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ToapplyOne(int id)
        {
            Goods goods = BorrowBLL.SelectIdGoods(id);
            return View(goods);
        }
        /// <summary>
        /// 提交申请页面
        /// </summary>
        /// <param name="id">物品id</param>
        /// <returns></returns>
        public IActionResult Toapply(int id)
        {
            Goods goods = BorrowBLL.SelectIdGoods(id);
            return View(goods);
        }
        /// <summary>
        /// 添加申请信息
        /// </summary>
        /// <param name="id">物品id</param>
        /// <param name="number">数量</param>
        /// <param name="description">理由</param>
        /// <returns></returns>
        #endregion







        #region 耗材申请
        public IActionResult IndexTwo()
        {
            return View();
        }
        public string GetListTwo(int page = 1, int limit = 5)
        {
            int dataConunt;
            List<Model.Goods> list = BorrowBLL.GetGoodsTwo(out dataConunt, page, limit);
            var result = new LayuiJsonModel<Model.Goods>()
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = list
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        #endregion




        #region 物料申请
        
        public IActionResult ToapplySave(int UserID, int Number, string Description, int GoodsID)//int UserID 用户ID, int Number数量, string Description申请理由
        {
            bool T = BorrowBLL.ToapplyOne(UserID, Number,Description,GoodsID);
            if (T)
                return Content("申请成功，请等耐心待领导审批");
            else
            return Content("申请失败，信息有误，或物品数量太少");
        }
        #endregion 
        #region  管理员管理申请
        public IActionResult IndexThere()
        {
            return View();
        }

       
        public string GetListFour(int page = 1, int limit = 5)
        {
            int dataConunt;
            var mod = (from Borrows in BorrowBLL.UpBorrow()
                       join Users in BorrowBLL.UpUsers() on Borrows.UserID equals Users.ID
                       join Goods in BorrowBLL.UpGood() on Borrows.GoodsID equals Goods.ID
                       join Statuses in BorrowBLL.UpStatu() on Borrows.StatusID equals Statuses.ID
                       where Borrows.SendTime != null && Borrows.MiddleTime == null
                       select new
                       {
                           Borrows.ID,
                           Goods.Name,
                           UserName = Users.Name,
                           Borrows.Description,
                           StatusName = Statuses.Name,
                           Borrows.SendTime,
                           Borrows.MiddleTime,
                           Borrows.EndTime,
                           Borrows.Number,
                           Complete = Borrows.Complete,
                       });
            mod.Skip((page - 1) * limit).Take(limit).ToList();   
         //  Skip((pageinde - 1) * pageSize).Take(pageSize).ToList();
            dataConunt = mod.Count();
            var result = new LayuiJsonModel<Model.Borrow>()
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = (List<Borrow>)mod
                };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        #endregion
    }
}
