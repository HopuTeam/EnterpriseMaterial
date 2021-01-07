using EnterpriseMaterial.Common;
using EnterpriseMaterial.Dto.PowerDTO;
using EnterpriseMaterial.ILogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseMaterial.Web.Controllers
{
    public class PowerController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }


    }
}
