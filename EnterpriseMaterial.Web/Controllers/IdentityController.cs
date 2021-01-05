using EnterpriseMaterial.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnterpriseMaterial.Model;

namespace EnterpriseMaterial.Web.Controllers
{
    public class IdentityController : Controller
    {
     


        public IActionResult Index()
        {
            return View();
        }
        //经理代码
        //public string GetJsonList(int page, int limit, string selectInfo)
        //{

        //    int totalCount = 0;

        //    List<Identity> outlist = _roleService.LoadPageEntities(page, limit, out totalCount, selectInfo);
        //    DataResult<List<RoleOutput>> dataResult = new DataResult<List<RoleOutput>>()
        //    {
        //        code = 0,
        //        msg = "ok",
        //        count = totalCount,
        //        data = outlist,
        //    };
        //    return JsonNetHelper.SerializetoJson(dataResult);
        //}









    }
}
