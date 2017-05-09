using SmartMarket.BLL.Managers;
using SmartMarket.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartMarket.BLL.Services
{
    // TODO Try to replace to BLL, 
    // And unified update from UserManager.
    public static class UserService
    {
        public static IUserManager UserManager
            => new UserManager();


        public static string Key
            => "CurrentUser";

        public static UserModel CurrentUser
        {
            get
            {
                if(HttpContext.Current != null)
                {
                    var user = HttpContext.Current.Session[Key] as UserModel;
                    if (user == null)
                    {
                        var email = HttpContext.Current.User.Identity.Name;
                        user = UserManager.FirstOrDefault(x => x.Email == email);
                        HttpContext.Current.Session[Key] = user;
                    }
                    return user;
                }

                return null;
            }
        }

        public static void Update(UserModel user)
        {
            HttpContext.Current.Items[Key] = user;
        }
    }
}