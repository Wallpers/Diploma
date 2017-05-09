using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartMarket.UI.Controllers.Helpers.Social
{
    public class UserVK
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }

        public AccessVK Access { get; set; }
    }
}