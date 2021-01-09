using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetMenu()
        {
            return Json(new { mod = Iuser.GetPower(HttpContext.Session.GetModel<Model.User>("User").SignID) });
        }
    }
}