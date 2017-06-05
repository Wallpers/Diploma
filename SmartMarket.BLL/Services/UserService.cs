using SmartMarket.BLL.Managers;
using SmartMarket.BLL.ViewModels;
using SmartMarket.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartMarket.BLL.Services
{
    static class UserService
    {
        static string Key => "CurrentUser";

        public static User CurrentUser
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    var user = HttpContext.Current.Session[Key] as User;
                    if (user == null)
                    {
                        user = Refresh();
                    }
                    return user;
                }

                return null;
            }
        }

        public static void Refresh(User user)
        {

            if (HttpContext.Current.Session != null)
                HttpContext.Current.Session[Key] = user;
        }

        public static User Refresh()
        {
            var email = HttpContext.Current.User.Identity.Name;
            var user = (new UserManager()).FirstOrDefault(x => x.Email == email);
            HttpContext.Current.Session[Key] = user;
            return user;
        }
    }
}