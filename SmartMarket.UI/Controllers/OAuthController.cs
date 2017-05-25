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
using SmartMarket.BLL.Social;
using SmartMarket.BLL.ViewModels;

using VK = SmartMarket.BLL.Social.VK;
using SmartMarket.BLL.Managers;
using SmartMarket.BLL;
using System.Web.Security;

namespace SmartMarket.UI.Controllers
{
    public class OAuthController : Controller
    {
        private HttpClient httpClient = new HttpClient();
        private IUserManager userManager = new UserManager();


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


            throw new InvalidOperationException();
        }

        public RedirectResult AuthorizationVk()
        {
            var url = "https://oauth.vk.com/authorize?" +
                $"client_id={VK.AuthParameters.ClientID}&" +
                $"display={VK.AuthParameters.Display}&" +
                $"redirect_uri={VK.AuthParameters.RedirectUrl}&" +
                $"scope={VK.AuthParameters.Scope}&" +
                $"response_type={VK.AuthParameters.ResponseType}&" +
                $"v={VK.AuthParameters.Version}";


            return Redirect(url);
        }

        public async Task<ActionResult> VerifyCode(string code)
        {
            var user = new OAuthModel();
            if (code != null)
            {
                var url = $"https://oauth.vk.com/access_token?" +
                    $"client_id={VK.AuthParameters.ClientID}&" +
                    $"client_secret={VK.AuthParameters.SecureKey}&" +
                    $"redirect_uri={VK.AuthParameters.RedirectUrl}&" +
                    $"code={code}";

                var json = (await httpClient.GetStringAsync(url)).ToJObject();

                user.Email = json.Value<string>("email");
                user.Expires = DateTime.Now.AddSeconds(json.Value<long>("expires_in"));
                user.Token = json.Value<string>("access_token");
                user.Identifier = json.Value<int>("user_id");
                
                url = $"https://api.vk.com/method/users.get?" +
                    $"uids={user.Identifier}&" +
                    $"fields=first_name,last_name&" +
                    $"access_token={user.Token}";

                json = (await httpClient.GetStringAsync(url)).ToJObject();
                json = json.GetValue("response").Value<JObject>(0);

                user.Name = json.Value<string>("first_name");
                user.LastName = json.Value<string>("last_name");
            }

            user = userManager.CreateOrUpdate(user);
            FormsAuthentication.SetAuthCookie(user.Email, true);


            return RedirectToAction("Edit", "Profile", user);
        }
    }
}