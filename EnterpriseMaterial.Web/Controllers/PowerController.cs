﻿using Microsoft.AspNetCore.Mvc;
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