﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeeManagement.Controllers
{
    public class SearchRecordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
