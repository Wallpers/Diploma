using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SmartMarket.BLL.Bank
{
    public static class Private24
    {
        public static int MerchantID
            => Convert.ToInt32(WebConfigurationManager.AppSettings["MerchantID"]);

        public static string MerchantPassword
            => WebConfigurationManager.AppSettings["MerchantPassword"];
    }
}
