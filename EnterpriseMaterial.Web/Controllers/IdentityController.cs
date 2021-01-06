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



        #region MyRegion

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult addindex()
        {
            return View();
        }



        public IActionResult Delindex()
        {
            return View();
        }

#endregion




        #region 非页面


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
            return JsonNetHelper.SerializetoJson(dataResult);
        }

        #endregion





    }
}
