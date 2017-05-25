using SmartMarket.BLL;
using SmartMarket.BLL.Managers;
using SmartMarket.BLL.Managers.Answers;
using SmartMarket.BLL.Resources;
using SmartMarket.BLL.Services;
using SmartMarket.BLL.ViewModels;
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
                    switch(userManager.Login(model))
                    {
                        case LoginAnswer.Access:
                            FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                            return JavaScript($"window.location = '{Url.Action("Index", "Home")}'");

                        case LoginAnswer.EmailNotConfirmed:
                            error = ResourceService.GetString(typeof(ErrorMessages), "EmailNotConfirmed");
                            ModelState.AddModelError("", error);
                            break;

                        case LoginAnswer.EmailNotFound:
                            error = ResourceService.GetString(typeof(ErrorMessages), "EmailNotFound");
                            ModelState.AddModelError("", error);
                            break;

                        case LoginAnswer.PasswordWrong:
                            error = ResourceService.GetString(typeof(ErrorMessages), "PasswordWrong");
                            ModelState.AddModelError("", error);
                            break;

                        case LoginAnswer.UserIsOAuth:
                            error = ResourceService.GetString(typeof(ErrorMessages), "UserIsOAuth");
                            ModelState.AddModelError("", error);
                            break;
                    }                    
                }

                return PartialView("~/Views/Partial/Account/_Login.cshtml", model);
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
                    if (!userManager.IsEmailExists(model.Email))
                    {
                        var id = userManager.Create(model);

                        var token = userManager.GenerateToken(model.Name, model.LastName);

                        var callback = Url.Action("ConfirmEmail", "Account",
                           new { id = id, token = token }, protocol: Request.Url.Scheme);

                        var linkName = ResourceService.GetString(typeof(Strings), "LinkName");
                        var link = "<a href='" + callback + "'>" + linkName + "</a>";

                        var text = ResourceService.GetString(typeof(Strings), "EmailText");
                        var body = $"{text} {link}";

                        userManager.SendEmail(model, body);

                        message = ResourceService.GetString(typeof(ErrorMessages), "EmailConfirmMessage");
                        model.StatusMessage = message;
                        
                    }

                    message = ResourceService.GetString(typeof(ErrorMessages), "EmailAlreadyExists");
                    ModelState.AddModelError("EmailAlreadyExists", message);
                }

                return PartialView("~/Views/Partial/Account/_Registry.cshtml", model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ConfirmEmail(int id, string token)
        {
            if (userManager.ConfirmEmail(id, token))
            {
                var message = ResourceService.GetString(typeof(Strings), "EmailConfirmed");
                return RedirectToAction("Index", "Home",  message );
            }

            var error = ResourceService.GetString(typeof(Strings), "EmailNotConfirmed");
            return RedirectToAction("Index", "Home", error );
        }

    }
}