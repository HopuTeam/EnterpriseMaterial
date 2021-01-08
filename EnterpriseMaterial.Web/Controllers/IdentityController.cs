using EnterpriseMaterial.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseMaterial.Model;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Data;
using Microsoft.Extensions.Logging;
using EnterpriseMaterial.Logic;
using EnterpriseMaterial.Dto.IdentityDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EnterpriseMaterial.Web.Controllers
{
    public class IdentityController : Controller
    {
        #region  构造函数注入
        private readonly ILogger<IdentityController> logger;
        private readonly IIdentityBLL identityBLL;

        public IdentityController(ILogger<IdentityController> logger,IIdentityBLL identityBLL) 
        {
            this.logger = logger;
            this.identityBLL = identityBLL;
        }
        #endregion



        #region 页面

        /// <summary>
        /// 身份首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加视图 
        /// </summary>
        /// <returns></returns>
        public IActionResult Addindex()
        {
            return View();
        }

        
          public IActionResult Updateindex(int ID)
        {
            IdentityQutput model = identityBLL.LoadEntities(ID).FirstOrDefault();
            ViewBag.model = model;
            return View();
           
        }
       

        /// <summary>
        /// 角色分配权限视图
        /// </summary>
        /// <returns></returns>
        public IActionResult IdentityPower(int id)
        {
            ViewData["power"] = identityBLL.Selectid(id);
            return View();
        }

        #endregion




        #region 非页面


    
       
        [HttpPost]
        public IActionResult Add(identityInput inputEntity) ///添加
        {
            if (identityBLL.Add(inputEntity) > 0)
            {
                return Content("ok");

            }
            else
            {
                return Content("no");
            }
        }


        /// <summary>
        /// 角色信息更新的方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Update(identityInput inputEntity)
        {

            if (identityBLL.Update(inputEntity) > 0)
            {

                return "ok";

            }
            else
            {
                return "no";
            }

        }


        /// <summary>
        /// 获取角色信息,不分页
        /// </summary>
        /// <returns></returns>
        public string GetJsonList1()// 获取角色信息,不分页
        {


            List<IdentityQutput> outlist = identityBLL.LoadEntities();
            DataResult<List<IdentityQutput>> dataResult = new DataResult<List<IdentityQutput>>()
            {
                code = 0,
                msg = "ok",
                data = outlist,
            };
            return JsonNetHelper.SerializetoJson(dataResult);
        }




        /// <summary>
        /// 获取角色分页信息
        /// </summary>
        /// <returns></returns>
        public string GetJsonList(int page, int limit, string selectInfo)//分页显示
        {

            int totalCount = 0;

            List<IdentityQutput> outlist = identityBLL.LoadPageEntities(page, limit, out totalCount, selectInfo);
            DataResult<List<IdentityQutput>> dataResult = new DataResult<List<IdentityQutput>>()
            {
                code = 0,
                msg = "ok",
                count = totalCount,
                data = outlist,
            };
           // return JsonNetHelper.SerializetoJson(dataResult);
            return Newtonsoft.Json.JsonConvert.SerializeObject(dataResult);
        }


        /// <summary>
        /// 判断角色id是否重复
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string CheckRepeat(int value)
        {

            if (identityBLL.CheckRepeat(value))
            {
                //如果有记录，说明用户id重复，不可以
                return "no";
            }
            else
            {
                return "ok";
            }

        }




        /// <summary>
        /// 开启/禁用角色的方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string ForbidRole(int ID, string flag)
        {
            if (identityBLL.ForbidRole(ID, flag))
            {
                if (flag == "true")
                {
                    return "角色已开启";
                }
                else
                {
                    return "角色已禁用";
                }

            }
            else
            {
                return "操作失败";
            }
        }


        /// <summary>
        /// 获取角色对应的权限
        /// </summary>
        /// <returns></returns>
        //public string GetPowerList(string roleId)
        //{

        //    PowerTreeOutput outlist = _powerService.GetPowerList(roleId);
        //    var a = JsonNetHelper.SerialzeoJsonForCamelCase(outlist);
        //    return "[" + a + "]";
        //}


        /// <summary>
        /// 为角色配置权限
        /// </summary>
        /// <returns></returns>
        //public string SetPower(string strId, string roleId)
        //{

        //    if (_powerService.SetPower(strId, roleId))
        //    {
        //        return "ok";
        //    }
        //    else
        //    {
        //        return "no";
        //    }

        //}

        /// <summary>
        /// 角色权限Tree
        /// </summary>
        /// <returns></returns>
        public string GetIdentityPower(int id)
        {
           // int id= HttpContext.Session.GetModel<User>("User").ID;
            LayuiTreeModel view = identityBLL.LayuiTreeModels(id);
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss"//格式化日期
            };
            var result = JsonConvert.SerializeObject(view, Formatting.Indented, setting);
            return "[" + result + "]";
         
        }
        /// <summary>
        /// 为身份配置权限
        /// </summary>
        /// <param name="IdentiyiList">选中的权限</param>
        /// <param name="ldentityid">ldentity表的id</param>
        /// <returns></returns>
        [HttpPost]
        public string SetPower(string  IdentiyiList,int ldentityid)
        {
            bool result= identityBLL.SetPower(IdentiyiList, ldentityid);
  
            if (result == true)
            {
                return "success";
            }
            else
            {
                return "fail";
            }
        }

        /// <summary>
        /// 重置权限
        /// </summary>
        /// <param name="ldentityid"></param>
        /// <returns></returns>
        public string Seset(int ldentityid)
        {
            bool result = identityBLL.Seset(ldentityid);

            if (result == true)
            {
                return "success";
            }
            else
            {
                return "fail";
            }
        }
        #endregion





    }
}
