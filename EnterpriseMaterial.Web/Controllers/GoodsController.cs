using EnterpriseMaterial.Common;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseMaterial.Web.Controllers
{
    public class GoodsController : Controller
    {
        private readonly lGoodsBLL goodsBLL;

        public GoodsController(ILogic.lGoodsBLL goodsBLL) {
            this.goodsBLL = goodsBLL;
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
            List<Goods> list = goodsBLL.GetGoodsOne(out dataConunt, page, limit);
            var result = new LayuiJsonModel<Goods>()
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
           Goods goods = goodsBLL.SelectIdGoods(id);
          return  View(goods);
        }
        /// <summary>
        /// 提交申请页面
        /// </summary>
        /// <param name="id">物品id</param>
        /// <returns></returns>
        public IActionResult Toapply(int id)
        {
            Goods goods = goodsBLL.SelectIdGoods(id);
            return View(goods);
        }
        /// <summary>
        /// 添加申请信息
        /// </summary>
        /// <param name="id">物品id</param>
        /// <param name="number">数量</param>
        /// <param name="description">理由</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ToapplySave(int id, int number, string description)
        {
            bool result = goodsBLL.ToapplyOne(id, number, description);
            return Content("ok");
        }
        #endregion

        #region 耗材申请
        public IActionResult IndexTwo()
        {
            return View();
        }
        public string GetListTwo(int page = 1, int limit = 5)
        {
            int dataConunt;
            List<Goods> list = goodsBLL.GetGoodsTwo(out dataConunt, page, limit);
            var result = new LayuiJsonModel<Goods>()
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = list
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        #endregion


        #region Goods 物品表的增上改查
        public IActionResult Index()
        {
            return View();
        }
        public string GetListGoods(int page = 1, int limit = 5)
        {
            int dataConunt;
            List<Goods> list = goodsBLL.GetGoodsOne(out dataConunt, page, limit);
            var result = new LayuiJsonModel<Goods>()
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = list
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// 修改页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult GetSelect(int id)
        {
            Goods goods = goodsBLL.SelectIdGoods(id);
            ViewData["categories"] = goodsBLL.GetCategories();
            return View(goods);
        }
        [HttpPost]
        public IActionResult GetSelectSave(Goods view)
        {
            bool result = goodsBLL.EditGoods(view);
            if (result==true)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
           
        }
        /// <summary>
        /// 根据物品名字模糊查询
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public string SelectName(string Name, int page = 1, int limit = 5)
        {
            int dataConunt;
            List< Goods> goods = goodsBLL.GoodsOne(Name, out dataConunt, page,limit);
            var result = new LayuiJsonModel<Goods>()
            {
                code = 0,
                msg = "ok",
                count = dataConunt,
                data = goods
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// 添加物品视图
        /// </summary>
        /// <returns></returns>
        public IActionResult AddGoods()
        {
            ViewData["categories"] = goodsBLL.GetCategories();
            return View();
        }
       /// <summary>
       /// 保存修改
       /// </summary>
       /// <param name="view"></param>
       /// <returns></returns>
        public IActionResult AddGoodsSave(Goods view)
        {
            bool result = goodsBLL.AddGoods(view);
            if (result == true)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        #endregion

    }
}
