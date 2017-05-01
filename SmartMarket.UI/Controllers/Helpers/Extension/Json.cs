using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartMarket.UI.Controllers.Helpers.Extension
{
    public static class Json
    {
        public static JObject ToJObject (this string @string)
        {
            return JObject.Parse(@string);
        }

        public static JToken ToJToken(this string @string)
        {
            return JToken.Parse(@string);
        }

        public static JArray ToJArray(this string @string)
        {
            return JArray.Parse(@string);
        }
    }
}