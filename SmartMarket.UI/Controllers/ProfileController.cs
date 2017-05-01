using SmartMarket.UI.Controllers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartMarket.UI.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public ActionResult Edit(User user)
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Edit(User user)
        //{
        //    return View();
        //}
    }
}