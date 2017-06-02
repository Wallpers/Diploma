using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartMarket.Android
{
    public class ResultActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ResultLayout);
            var message = Intent.GetStringExtra("message");

            var resultTextView = FindViewById<TextView>(Resource.Id.resultTextView);
            resultTextView.Text = message;
        }
    }
}