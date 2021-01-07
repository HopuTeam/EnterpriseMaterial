using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseMaterial.Web.Controllers
{
    public class HomeController : Controller
    {
        private ILogic.IUserLogic Iuser { get; }

        public HomeController(ILogic.IUserLogic Iuser)
        {
            this.Iuser = Iuser;
        }

        public IActionResult Index()
        {
            return View();
        }
        
      //  [HttpPost]
        //public IActionResult GetMenu()
        //{
        //    return Json(new { mod = Iuser.GetPower(HttpContext.Session.GetModel<Model.User>("User").SignID) });
        //}
    }
}