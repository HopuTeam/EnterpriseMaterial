using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using EnterpriseMaterial.Common;
using Microsoft.AspNetCore.Authorization;

namespace EnterpriseMaterial.Web.Controllers
{
    [AllowAnonymous]
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

            sign.IdentityID = 1;
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
        public IActionResult Forget(string Code, string Email, Model.Sign sign)
        {
            if (Code != code)
                return Json(new { status = false, message = "验证码错误" });

            var acc = Isign.GetAccount(sign);
            var eml = Iuser.GetEmail(Email);
            if (acc == null || eml == null || acc.ID != eml.ID)
                return Json(new { status = false, message = "邮箱与账号信息不匹配" });

            sign.Password = Security.MD5Encrypt32(sign.Password);

            if (Isign.EditPassword(sign))
                return Json(new { status = true, message = "重置密码成功" });
            else
                return Json(new { status = false, message = "未修改任何信息" });
        }

        [HttpPost]
        public IActionResult SendMail(string Email, Model.Sign sign)
        {
            Random random = new Random();
            code = Security.MD5Encrypt32(random.Next(0, 9999).ToString()).Substring(random.Next(1, 16), 6).ToUpper();

            var acc = Isign.GetAccount(sign);
            var eml = Iuser.GetEmail(Email);
            if (acc == null || eml == null || acc.ID != eml.ID)
                return Json(new { status = false, message = "邮箱与账号信息不匹配" });

            if (MailExt.SendMail(Email, "找回密码操作", $"尊敬的用户 { sign.Account }：<br />您正在进行<span style='color:red;'>找回密码</span>操作！<br />本次操作的验证码是：<span style='color:red;'>{ code }</span>。<br />请注意谨防验证码泄露，保护账号安全！"))
                return Json(new { status = true, message = "邮件发送成功" });
            else
                return Json(new { status = false, message = "邮件发送失败" });
        }
    }
}