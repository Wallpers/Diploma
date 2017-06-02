using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Net.Http;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using ModernHttpClient;
using System.Threading;
using System.IO;
using System.Net.Http.Headers;
using SmartMarket.Android.Helpers;
using Android.Content;

namespace SmartMarket.Android
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var loginButton = FindViewById<Button>(Resource.Id.loginButton);
            loginButton.Click += async (sender, e) =>
            {
                var resultTextView = FindViewById<TextView>(Resource.Id.resultTextView);

                var email = "Kokorin1506@mail.ru";
                var password = "7131498Alex";

                var text = await PostAsync(email, password);
                var answer = (LoginAnswer)Convert.ToInt32(text);

                var message = "";
                switch(answer)
                {
                    case LoginAnswer.Access:
                        message += "Login successfuly.";
                        var id = Convert.ToInt32(await GetAsync(email));
                        CurrentUser.ID = id;

                        RunOnUiThread(() => StartActivity(typeof(PurchasesActivity)));
                        break;
                    case LoginAnswer.EmailNotConfirmed:
                        message += "Email doesn't confirmed.";
                        break;

                    case LoginAnswer.EmailNotFound:
                        message += "Email doesn't found.";
                        break;

                    case LoginAnswer.PasswordWrong:
                        message += "Password is wrong.";
                        break;

                    case LoginAnswer.UserIsOAuth:
                        message += "User is oauth. You must have been registrated by forms on our web site.";
                        break;
                }

                RunOnUiThread(() => resultTextView.Text = message);
            };

        }

        private async Task<string> GetAsync(string email)
        {
            string url = "http://192.168.0.90:55829/api/authorize?email=" + email;
            string contentType = "application/x-www-form-urlencoded";

            JObject json = new JObject(new JProperty("email", email));

            var model = new StringContent(json.ToString(), Encoding.UTF8, contentType);

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var response = await client.GetAsync(url);
                var text = await response.Content.ReadAsStringAsync();
                return text;
            }
        }

        private async Task<string> PostAsync(string email, string password)
        {
            string url = "http://192.168.0.90:55829/api/authorize/";
            string contentType = "application/json";

            JObject json = new JObject(new JProperty("email", email), new JProperty("password", password));

            var model = new StringContent(json.ToString(), Encoding.UTF8, contentType);

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var response = await client.PostAsync(url, model);
                var text = await response.Content.ReadAsStringAsync();
                return text;
            }
        }


    }
}

