using SmartMarket.BLL;
using SmartMarket.BLL.Managers;
using SmartMarket.BLL.Services;
using SmartMarket.BLL.ViewModels;
using SmartMarket.UI.Controllers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartMarket.UI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        IUserManager userManager = new UserManager();

        [HttpGet]
        public ActionResult Edit()
        {
            var user = UserService.CurrentUser;
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            // TODO Make server validation.

            userManager.Update(user);
            return RedirectToAction("Index", "Home");
        }
    }
}