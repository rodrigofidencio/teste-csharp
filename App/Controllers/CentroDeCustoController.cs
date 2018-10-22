using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fundagMVC.Controllers
{
    public class CentroDeCustoController : Controller
    {
        // GET: CentroDeCusto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroCentroDeCusto()
        {
            return View();
        }
    }
}