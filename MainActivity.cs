using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace SmartMarket.Android
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button loginButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            loginButton = FindViewById<Button>(Resource.Id.loginButton);
            loginButton.Click += LoginHandler;

        }

        private void LoginHandler(object sender, EventArgs e)
        {
            var email = FindViewById<EditText>(Resource.Id.emailEditText).Text;
            var password = FindViewById<EditText>(Resource.Id.passwordEditText).Text;

            throw new NotImplementedException();
        }
    }
}

