using SmartMarket.BLL;
using SmartMarket.BLL.Managers;
using SmartMarket.BLL.Managers.Answers;
using SmartMarket.BLL.Resources;
using SmartMarket.BLL.Services;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SmartMarket.UI.Controllers
{
    public class AccountController : Controller
    {
        IUserManager userManager = new UserManager();

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    string error;
                    switch(userManager.Check(model))
                    {
                        case LoginAnswer.Access:
                            FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                            return JavaScript($"window.location = '{Url.Action("Index", "Home")}'");

                        case LoginAnswer.EmailNotConfirmed:
                            error = ResourceService.GetString(typeof(ErrorMessages), "EmailNotConfirmed");
                            ModelState.AddModelError("EmailNotConfirmed", error);
                            break;

                        case LoginAnswer.EmailNotFound:
                            error = ResourceService.GetString(typeof(ErrorMessages), "EmailNotFound");
                            ModelState.AddModelError("EmailNotFound", error);
                            break;

                        case LoginAnswer.PasswordWrong:
                            error = ResourceService.GetString(typeof(ErrorMessages), "PasswordWrong");
                            ModelState.AddModelError("PasswordWrong", error);
                            break;
                    }                    
                }

                return PartialView("~/Views/Account/Login.cshtml", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Registry(RegistryModel model)
        {
            string message; 
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    if (userManager.IsEmailValid(model.Email))
                    {
                        var user = userManager.Create(model);
                        if (user != null)
                        {
                            var token = userManager.GenerateToken(user);

                            var callback = Url.Action("ConfirmEmail", "Account",
                               new { id = user.ID, token = token }, protocol: Request.Url.Scheme);

                            var linkName = ResourceService.GetString(typeof(Strings), "LinkName");
                            var link = "< a href =\"" +
                                  $"{callback}" +
                                  $"\">{linkName}</a>";

                            var text = ResourceService.GetString(typeof(Strings), "EmailText");
                            var body = $"{text} {link}";

                            userManager.SendEmail(user, body);

                            message = ResourceService.GetString(typeof(ErrorMessages), "EmailConfirmMessage");
                            model.StatusMessage = message;
                        }
                    }

                    message = ResourceService.GetString(typeof(ErrorMessages), "EmailAlreadyExists");
                    ModelState.AddModelError("EmailAlreadyExists", message);
                }

                return PartialView("~/Views/Account/Registry.cshtml", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ConfirmEmail(int id, string token)
        {
            var user = userManager.Get(id);

            if (user != null)
            {
                if (userManager.GenerateToken(user) == token)
                {
                    user.IsEmailConfirmed = true;
                    userManager.Update(user);

                    var message = ResourceService.GetString(typeof(Strings), "EmailConfirmed");
                    return View("~/Views/Home/Index.cshtml", new IndexModel() { StatusMessage = message });
                }
            }

            // TODO Display page error.

            return RedirectToAction("Index", "Home");
        }

    }
}