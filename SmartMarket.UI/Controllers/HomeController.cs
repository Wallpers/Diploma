using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartMarket.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        { 
            return View(new IndexModel());
        }


    }
}
