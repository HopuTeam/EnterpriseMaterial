﻿using EnterpriseMaterial.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EnterpriseMaterial.Web.Controllers
{
    public class UserController : Controller
    {
        private ILogic.IUserLogic Iuser { get; }
        private ILogic.ISignLogic Isign { get; }
        public UserController(ILogic.IUserLogic Iuser, ILogic.ISignLogic Isign)
        {
            this.Iuser = Iuser;
            this.Isign = Isign;
        }

        public IActionResult Index()
        {
            return View(Iuser.GetInfo(HttpContext.Session.GetModel<Model.User>("User").SignID));
        }
        public IActionResult Edit(int ID)
        {
            return View(Iuser.GetInfo(ID));
        }

        [HttpGet]
        public IActionResult Manager()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MagUserList(int page = 1, int limit = 10)
        {
            return Json(Iuser.GetUsers(page, limit));
        }

        [HttpPost]
        public IActionResult Edit(Dto.UserDto.UserOut user)
        {
            int ID = user.ID;
            if (ID == 0)
                user.ID = HttpContext.Session.GetModel<Model.User>("User").SignID;

            if (user.Password != null)
                user.Password = Security.MD5Encrypt32(user.Password);

            if (!Iuser.GetEdit(user))
                return Json(new { status = false, message = "未进行任何修改" });

            if (ID == 0)
                HttpContext.Session.SetModel("User", Iuser.GetAccount(null, user.ID));

            return Json(new { status = true, message = "用户信息更新成功" });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.SetModel("User", null);
            return Json(new { message = "您已退出账号", status = true });
        }

        [HttpGet]
        public IActionResult Auth(string Code, string Account, string Email)
        {
            if (Code != HttpContext.Session.GetString("Code"))
                return Content("<script>alert('认证码错误');window.location.href='/User/Index';</script>", "text/html", System.Text.Encoding.UTF8);

            var acc = Isign.GetAccount(Account);
            var eml = Iuser.GetEmail(Email);
            if (acc == null || eml == null || acc.ID != eml.ID)
                return Content("<script>alert('认证信息异常');window.location.href='/User/Index';</script>", "text/html", System.Text.Encoding.UTF8);

            if (Iuser.Auth(acc.ID))
                return Content("<script>alert('认证成功');window.location.href='/User/Index';</script>", "text/html", System.Text.Encoding.UTF8);
            else
                return Content("<script>alert('认证失败，请重试');window.location.href='/User/Index';</script>", "text/html", System.Text.Encoding.UTF8);
        }

        [HttpPost]
        public IActionResult SendMail()
        {
            var mod = Iuser.GetInfo(HttpContext.Session.GetModel<Model.User>("User").SignID);
            if (mod == null)
                return Json(new { status = false, message = "登陆状态异常" });

            Random random = new Random();
            HttpContext.Session.SetString("Code", Security.MD5Encrypt32(random.Next(0, 9999).ToString()).Substring(random.Next(1, 16), 6).ToUpper());
            if (Iuser.GetEmail(mod.Email) == null)
                return Json(new { status = false, message = "账号无绑定邮箱" });

            if (MailExt.SendMail(mod.Email, "账户验证操作", $"尊敬的用户 { mod.Account }：<br />您正在进行<span style='color:skyblue;'>账户认证</span>操作！<br />请点击[<a href='https://localhost:44339/User/Auth?Code={ HttpContext.Session.GetString("Code") }&Account={ mod.Account }&Email={ mod.Email }'>本链接</a>]进行认证。<br />请注意谨防验证码泄露，保护账号安全！"))
                return Json(new { status = true, message = "邮件发送成功" });
            else
                return Json(new { status = false, message = "邮件发送失败" });
        }

        [HttpPost]
        public IActionResult SwichStatus(int SignID)
        {
            if (Iuser.SwichStatus(SignID))
                return Json(new { status = true, message = "状态更新成功" });
            else
                return Json(new { status = false, message = "状态更新失败" });
        }
    }
}