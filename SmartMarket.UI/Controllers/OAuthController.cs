using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartMarket.UI.Controllers
{
    public enum SocialProvider
    {
        Vkontakte,
        Facebook,
        Linkedin
    }

    public class OAuthController : Controller
    {
        public ActionResult LogOn(SocialProvider socialNetworksProvider)
        {
            switch (socialNetworksProvider)
            {
                case SocialProvider.Vkontakte:

                    break;
                case SocialProvider.Facebook:

                    break;
                case SocialProvider.Linkedin:

                    break;
            }

            return View("/Home/Index");
        }
    }
}