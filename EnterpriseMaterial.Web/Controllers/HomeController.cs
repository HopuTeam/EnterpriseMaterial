using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseMaterial.Web.Controllers
{
    public class HomeController : Controller
    {
        private Data.CoreEntities EF { get; }

        public HomeController(Data.CoreEntities _ef)
        {
            EF = _ef;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult GetMenu()
        //{
        //    var mod = (from u in EF.Signs
        //               join i in EF.Identities on u.IdentityID equals i.ID
        //               join ip in EF.IdentityPowers on i.ID equals ip.IdentityID
        //               join p in EF.Powers on ip.PowerID equals p.ID
        //               where u.ID == 1
        //               select p).ToList();
        //    return Json(new { mod });
        //}
    }
}