using EnterpriseMaterial.Common;
using EnterpriseMaterial.ILogic;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMaterial.Web.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(ILogic.IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetJsonList(string selectInfo = null, int page = 1, int limit = 10)
        {
            var outlist = _departmentService.LoadPageEntities(page, limit, out int totalCount, selectInfo);
            var result = new
            {
                code = 0,
                msg = "",
                count = totalCount,
                data = outlist
            };
            return JsonNetHelper.SerializetoJson(result);
        }
    }
}