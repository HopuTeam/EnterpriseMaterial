using EnterpriseMaterial.Common;
using EnterpriseMaterial.Dto.DepartmentDTO;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseMaterial.Web.Controllers
{
    
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        #region 构造函数注入
        //private readonly DepartmentService _departmentService;

        //public DepartmentController(DepartmentService departmentService)
        //{
        //    _departmentService = departmentService;
        //}

        //private ILogger<DepartmentController> _logger;

        //public DepartmentController(ILogger<DepartmentController> logger)
        //{
        //    _logger = logger;
        //}

        public DepartmentController(ILogic.IDepartmentService departmentService) {
            this._departmentService = departmentService;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
  #region 非页面       
        public string GetJsonList(int page, int limit, string selectInfo)
        {

            int totalCount = 0;

            List<DepartmentOutput> outlist = _departmentService.LoadPageEntities(page, limit, out totalCount, selectInfo);
            DataResult<List<DepartmentOutput>> dataResult = new DataResult<List<DepartmentOutput>>()
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
