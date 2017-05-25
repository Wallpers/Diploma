using SmartMarket.BLL;
using SmartMarket.BLL.Managers;
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
        IModelManager modelManager = new ModelManager();

        public ActionResult Index(string message)
        {
            var model = modelManager.GetModel<IndexModel>();
            model.StatusMessage = message;
            return View(model);
        }
    }
}
