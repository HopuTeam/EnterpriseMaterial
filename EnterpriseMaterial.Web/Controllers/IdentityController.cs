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

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Addindex()
        {
            return View();
        }



        public IActionResult Delindex()
        {
            return View();
        }

        #endregion




        #region 非页面


        /// <summary>
        /// 给用户配置角色的方法
        /// </summary>
        /// <param name="strjson"></param>
        /// <returns></returns>
        //public string ConfigRole(string userID, string arrRoleID)
        //{
        //    if (identityBLL.ConfigRole(userID, arrRoleID))
        //    {
        //        return "操作成功";
        //    }
        //    else
        //    {
        //        return "操作失败";
        //    }

        //}
        //未完成


        public string Add(identityInput inputEntity)
        {

            if (identityBLL.Add(inputEntity) > 0)
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



        #endregion





    }
}
