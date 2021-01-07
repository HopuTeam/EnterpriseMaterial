using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using EnterpriseMaterial.Common;

namespace EnterpriseMaterial.Web.Controllers
{
    public class SignController : Controller
    {
        private ILogic.ISignLogic Isign { get; }
        private ILogic.IUserLogic Iuser { get; }

        public SignController(ILogic.ISignLogic Isign, ILogic.IUserLogic Iuser)
        {
            this.Isign = Isign;
            this.Iuser = Iuser;
        }

        // User login
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Sign(Model.Sign sign)
        {
            sign.Password = Security.MD5Encrypt32(sign.Password);
            Model.User mod = Isign.GetSign(sign);

            if (mod == null)
                return Json(new { status = false, message = "用户名或密码错误" });

            HttpContext.Session.SetModel("User", mod);
            return Json(new { status = true, message = "登录成功" });
        }

        // User register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Model.Sign sign, string Email)
        {
            if (Isign.GetAccount(sign) != null)
                return Json(new { status = false, message = "用户名被占用" });

            if (Iuser.GetEmail(Email) != null)
                return Json(new { status = false, message = "邮箱被占用" });

            sign.IdentityID = 0;
            sign.Password = Security.MD5Encrypt32(sign.Password);

            if (Isign.GetRegister(sign))
                return Json(new { status = true, message = "登录成功" });
            else
                return Json(new { status = false, message = "表单数据异常" });
        }

        string code;
        [HttpGet]
        public IActionResult Forget()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Forget(string Code, Model.Sign sign)
        {
            if (Code != code)
                return Json(new { status = false, message = "验证码错误" });

            var mod = EF.Signs.FirstOrDefault(x => x.Account == sign.Account && x.Email == sign.Email);
            if (mod == null)
                return Content("邮箱或帐号错误");

            mod.Password = Security.MD5Encrypt32(sign.Password);

            if (EF.SaveChanges() > 0)
                return Content("success");
            else
                return Content("重置密码失败，请重试");
        }
    }
}