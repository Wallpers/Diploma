using SmartMarket.UI.Controllers.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using SmartMarket.UI.Controllers.Helpers.Extension;
using Newtonsoft.Json;

namespace SmartMarket.UI.Controllers
{
    public class OAuthController : Controller
    {
        private readonly HttpClient httpClient = new HttpClient();

        public ActionResult LogOn(SocialProviders socialNetworksProvider)
        {
            switch (socialNetworksProvider)
            {
                case SocialProviders.Vkontakte:
                    return AuthorizationVk();
                case SocialProviders.Facebook:
                    break;
                case SocialProviders.Linkedin:
                    break;
            }

            return RedirectToAction("Index","Home");
        }

        public RedirectResult AuthorizationVk()
        {
            var url = "https://oauth.vk.com/authorize?" +
                $"client_id={VK.ClientID}&" +
                $"display={VK.Display}&" +
                $"redirect_uri={VK.RedirectUrl}&" +
                $"scope={VK.Scope}&" +
                $"response_type={VK.ResponseType}&" +
                $"v={VK.Version}";


            return Redirect(url);
        }

        public async Task<ActionResult> VerifyCode(string code)
        {
            var userVK = new UserVK();
            if (code != null)
            {
                var url = $"https://oauth.vk.com/access_token?" +
                    $"client_id={VK.ClientID}&" +
                    $"client_secret={VK.SecureKey}&" +
                    $"redirect_uri={VK.RedirectUrl}&" +
                    $"code={code}";

                var json = (await httpClient.GetStringAsync(url)).ToJObject();
                var access = new AccessVK()
                {
                    Email = json.Value<string>("email"),
                    Expires = DateTime.Now.AddSeconds(json.Value<long>("expires_in")),
                    Token = json.Value<string>("access_token"),
                    UserID = json.Value<int>("user_id")
                };


                url = $"https://api.vk.com/method/users.get?" +
                    $"uids={access.UserID}&" +
                    $"fields=first_name,last_name,bdate&" +
                    $"access_token={access.Token}";

                json = (await httpClient.GetStringAsync(url)).ToJObject();
                json = json.GetValue("response").Value<JObject>(0);


                userVK.Name = json.Value<string>("first_name");
                userVK.LastName = json.Value<string>("last_name");
                userVK.BirthDay = Convert.ToDateTime(json.Value<string>("bdate"));
                userVK.Access = access;
            }

            return RedirectToAction("Edit", "Profile", new {
                Email = userVK.Access.Email,
                Name = userVK.Name,
                LastName = userVK.LastName
            });
        }
    }
}