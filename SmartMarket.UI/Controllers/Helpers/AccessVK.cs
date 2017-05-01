using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartMarket.UI.Controllers.Helpers
{
    public class AccessVK
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public int UserID { get; set; }
    }
    

}