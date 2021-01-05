using EnterpriseMaterial.Common;
using EnterpriseMaterial.ILogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseMaterial.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryBLL categoryBLL;
        public CategoryController(ILogic.ICategoryBLL categoryBLL)
        {
            this.categoryBLL = categoryBLL;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string CategoryTree()
        {
            LayuiTreeModel view = categoryBLL.LayuiTreeModels();
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(view);
            return "[" + result + "]";
        }


    }
}
