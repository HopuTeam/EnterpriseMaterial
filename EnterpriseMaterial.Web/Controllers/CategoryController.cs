using EnterpriseMaterial.Common;
using EnterpriseMaterial.ILogic;
using EnterpriseMaterial.Model;
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

        public IActionResult Add(int id)
        {
            Model.Category result = categoryBLL.Selectid(id);
            return View(result);
        }
        public IActionResult AddSave(int id,string Name)
        {
            bool result = categoryBLL.AddSave(id, Name);
            if (result)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        /// <summary>
        /// 修改分类
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public IActionResult Edit(Category view)
        {
            bool result = categoryBLL.EditSave(view);
            if (result)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int  id)
        {
            bool result = categoryBLL.Delete(id);
            if (result)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }
        }

    }
}
