using SmartMarket.BLL;
using SmartMarket.BLL.Managers;
using SmartMarket.BLL.Managers.Answers;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartMarket.UI.Controllers
{
    public class AuthorizeController : ApiController
    {
        IUserManager userManager = new UserManager();

        public int Get(string email)
        {
            return userManager.GetUserId(email);
        }

        public LoginAnswer Post(LoginModel model)
        {
            return userManager.Login(model);

        }

        
    }
}
