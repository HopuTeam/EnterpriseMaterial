using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMaterial.Web.Controllers
{
    public class TypeController : Controller
    {
        private ILogic.ITypeLogic Ilogic { get; }

        public TypeController(ILogic.ITypeLogic Ilogic)
        {
            this.Ilogic = Ilogic;
        }

        // 获取类型信息
        public IActionResult GetList()
        {
            return Json(new { message = "GetList", data = Ilogic.GetList() });
        }
        // 修改类型信息
        public IActionResult Edit(Model.Type type)
        {
            return Json(new { message = "Edit", status = Ilogic.Edit(type) });
        }
        // 新增类型信息
        public IActionResult Add(Model.Type type)
        {
            return Json(new { message = "Add", status = Ilogic.Add(type) });
        }
        // 删除类型信息
        public IActionResult Delete(Model.Type type)
        {
            return Json(new { message = "Delete", status = Ilogic.Delete(type) });
        }
    }
}