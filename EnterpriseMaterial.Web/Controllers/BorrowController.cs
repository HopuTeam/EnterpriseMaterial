using EnterpriseMaterial.Common;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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
   
        /// <summary>
        /// 申请物品领取列表页面
        /// </summary>
        /// <returns></returns>
        public IActionResult IndexOne()
        {
            return View();
        }
        public string GetListOne(int page = 1, int limit = 10)
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
      
        #region 物料申请
        public IActionResult ToapplySave(int GoodsID, int Number, string Description, int Uid)//int UserID 用户ID, int Number数量, string Description申请理由
        {
            Uid = HttpContext.Session.GetModel<Model.User>("User").SignID;
            bool T = BorrowBLL.ToapplyOne(GoodsID, Number, Description, Uid);
            if (T)
                return Content("申请成功，请等耐心待领导审批");
            else
                return Content("申请失败，信息有误，或物品数量太少");
        }
        #endregion
        #region  管理员管理申请

        //管理员
        public IActionResult IndexFour()
        {
            return View();
        }
        public string GetListFour(int page = 1, int limit = 5)
        {
            dynamic mod = JsonConvert.DeserializeObject(BorrowBLL.UpBorrow(out int dataConunt, page, limit));
            // dynamic mod = JsonConvert.DeserializeObject(BorrowBLL.UpSuperior(out int dataConunt, page, limit));
            //List<Borrow> list =(List<Borrow>)JsonConvert.DeserializeObject(mod.ToString());
            var result = new
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = mod
            };
            return JsonConvert.SerializeObject(result);
        }

        //上级领导
        public IActionResult IndexFive()
        {
            return View();
        }
        public string GetListFive(int page = 1, int limit = 5)
        {
            //dynamic mod = JsonConvert.DeserializeObject(BorrowBLL.UpBorrow(out int dataConunt, page, limit));
            dynamic mod = JsonConvert.DeserializeObject(BorrowBLL.UpSuperior(out int dataConunt, page, limit));
            var result = new
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = mod
            };
            return JsonConvert.SerializeObject(result);
        }
        public IActionResult Apply(int id)
        {
            Dto.BorrowDto.BorrowOut mod = BorrowBLL.Upapply(id);
            return View(mod);
        }
        #endregion


        public IActionResult GetApply(Dto.BorrowDto.BorrowOut borrow)
        {

            bool mod = BorrowBLL.Agree(borrow);
            if (mod)
                return Content("成功");
            else
                return Content("失败");
        }

        public IActionResult DownIndex()
        {
            return View();
        }

        public string Down(int Uid, int page = 1, int limit = 5)
        {
            Uid = HttpContext.Session.GetModel<Model.User>("User").SignID;
            dynamic mod = JsonConvert.DeserializeObject(BorrowBLL.Downpass(out int dataConunt, Uid, page, limit));

            dynamic result = new
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = mod
            };
            var list = JsonConvert.SerializeObject(result);
            return list;

        }

        public string UserApply(int Uid, int page = 1, int limit = 5)
        {
            Uid = HttpContext.Session.GetModel<Model.User>("User").SignID;
            dynamic mod = JsonConvert.DeserializeObject(BorrowBLL.Getapply(out int dataConunt, Uid, page, limit));

            dynamic result = new
            {
                code = 0,
                msg = "",
                count = dataConunt,
                data = mod
            };

            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 领取物品
        /// </summary>
        /// <param name="BorriwId"></param>
        /// <returns></returns>
        public IActionResult Toreceive(int BorriwId)
        {
            bool res = BorrowBLL.Toreceive(BorriwId);
            if (res)
            {
                return Json(new { status = res, message = "领取成功" });
            }
            return Json(new { status = res, message = "领取失败" });
        }

        /// <summary>
        /// 归还物资
        /// </summary>
        /// <param name="BorriwId"></param>
        /// <returns></returns>
        public IActionResult Thereturn(int BorriwId)
        {
            bool res = BorrowBLL.Thereturn(BorriwId);
            if (res)
            {
                return Json(new { status = res, message = "归还成功" });
            }
            return Json(new { status = res, message = "归还失败" });
        }
    }
}
