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
        
        public IActionResult ToapplySave()//int UserID 用户ID, int Number数量, string Description申请理由
        {
            var UserID = Request.Query["UserID"];
            var Number = Request.Query["Number"];
            var Description = Request.Query["Description"];
            return Content(UserID+Number+Description);
        }
        #endregion
    }
}
