﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fundagMVC.Controllers
{
    public class ContaContabilController : Controller
    {
        // GET: ContaContabil
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroContaContabil()
        {
            return View();
        }
    }
}