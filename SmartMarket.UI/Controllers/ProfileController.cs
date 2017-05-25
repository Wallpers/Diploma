using SmartMarket.BLL;
using SmartMarket.BLL.Managers;
using SmartMarket.BLL.Resources;
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
        IModelManager modelManager = new ModelManager();

        [HttpGet]
        public ActionResult Edit()
        {
            var model = modelManager.GetModel<EditModel>();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditModel newModel)
        {
            var oldModel = modelManager.GetModel<EditModel>();
            bool isEmailExists = true;

            if(newModel.Email != oldModel.Email)
            {
                isEmailExists = userManager.IsEmailExists(newModel.Email);
            }

            if (ModelState.IsValid)
            {
                if (isEmailExists)
                {
                    userManager.Update(newModel);

                    var message = ResourceService.GetString(typeof(Strings), "AllSettingSaved");
                    newModel.StatusMessage = message;
                }
                else
                {
                    var error = ResourceService.GetString(typeof(ErrorMessages), "EmailAlreadyExists");
                    ModelState.AddModelError("EmailAlreadyExists", error);
                }            
            }
            return View(newModel);
        }
    }
}