using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SmartMarket.UI.Controllers.Helpers
{
    public static class VK
    {
        // Documentation VK Developers.
        // https://vk.com/dev/authcode_flow_user

        public static string ClientID
            => WebConfigurationManager.AppSettings["ClientID"];

        public static string SecureKey
            => WebConfigurationManager.AppSettings["SecureKey"];

        public static string RedirectUrl
            => "http://localhost:55827/OAuth/VerifyCode";

        public static string Display
            => "page";

        public static string Scope
            => "email";

        public static string ResponseType
            => "code";

        public static string Version
            => "5.63";
    }
}