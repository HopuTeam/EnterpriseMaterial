using Microsoft.AspNetCore.Mvc;

namespace EnterpriseMaterial.Web.Controllers
{
    public class PowerController : Controller
    {
        private ILogic.IUserLogic Iuser { get; }
        public PowerController(ILogic.IUserLogic Iuser)
        {
            this.Iuser = Iuser;
        }

        public IActionResult Index()
        {
            return View(Iuser.GetPowerInfo());
        }

        [HttpGet]
        public IActionResult Add(int ParentID)
        {
            return View(ParentID);
        }
        [HttpPost]
        public IActionResult Add(Model.Power power)
        {
            if (Iuser.AddPower(power))
                return Json(new { status = true, message = "添加成功" });
            else
                return Json(new { status = false, message = "添加失败" });
        }

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            return View(Iuser.GetOnePowerInfo(ID));
        }
        [HttpPost]
        public IActionResult Edit(Model.Power power)
        {
            if (Iuser.EditPower(power))
                return Json(new { status = true, message = "修改成功" });
            else
                return Json(new { status = false, message = "操作失败，数据异常" });
        }

        [HttpPost]
        public IActionResult Delete(int ID)
        {
            if (Iuser.DelPower(ID))
                return Json(new { status = true, message = "删除成功" });
            else
                return Json(new { status = false, message = "操作失败，数据异常" });
        }
    }
}