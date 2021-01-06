using EnterpriseMaterial.Common;
using EnterpriseMaterial.ILogic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
            var setting = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss"//格式化日期
            };
            var result = JsonConvert.SerializeObject(view, Formatting.Indented, setting);
            return "[" + result + "]";
        }


    }
}
