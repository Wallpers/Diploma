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
using System.Globalization;
using System.Net.Http;
using ModernHttpClient;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SmartMarket.Android
{
    //TODO It's horrible, must be refactored. 

    [Activity(Label = "Select your purchases.")]
    public class PurchasesActivity : Activity
    {
        ListView cards;
        ListView purchases;

        TextView total;
        decimal sum;

        Balance balance;

        List<Product> products;
        List<Balance> balances;

        Button payButton;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Purchases);

            payButton = FindViewById<Button>(Resource.Id.payButton);
            total = FindViewById<TextView>(Resource.Id.totalTextView);
            sum = 0;
            total.Text = sum.ToString("C");

            
            BalancesInitAsync();
            ProductsInit();

            payButton.Click += async (sender, e) =>
            {
                string url = "http://192.168.0.90:55829/api/payment/";
                string contentType = "application/json";

                JObject json = new JObject(new JProperty("BalanceID", balance.ID), new JProperty("TotalPrice", sum));
                var model = new StringContent(json.ToString(), Encoding.UTF8, contentType);
                
                using (var client = new HttpClient(new NativeMessageHandler()))
                {
                    var response = await client.PostAsync(url, model);
                    var text = await response.Content.ReadAsStringAsync();

                    var message = "";
                    if (text == "true")
                    {
                        message = "Transaction successfully done.";
                    }
                    else
                    {
                        message = "Transaction was failed.";
                    }
                    var intent = new Intent(this, typeof(ResultActivity));
                    intent.PutExtra("message", message);
                    RunOnUiThread(() => StartActivity(intent));
                }
            };
        }

        private async void BalancesInitAsync()
        {
            cards = FindViewById<ListView>(Resource.Id.cardsListView);

            balances = await GetBalancesAsync(CurrentUser.ID);

            RunOnUiThread(() =>
            {
                cards.Adapter = new SingleListViewAdapter(this, balances);
                cards.ItemClick += CardsHandler;
            });
        }

        private async Task<List<Balance>> GetBalancesAsync(int id)
        {
            string url = "http://192.168.0.90:55829/api/balances/" + id;

            using (var client = new HttpClient(new NativeMessageHandler()))
            {
                var response = await client.GetAsync(url);
                var text = await response.Content.ReadAsStringAsync();
                return GetBalances(text);
            }
        }


        private void CardsHandler(object sender, AdapterView.ItemClickEventArgs e)
        {
            balance = balances[e.Position];
            balances = balances.Select(b => new Balance { ID = b.ID, Cash = b.Cash, Checked = false }).ToList();
            balance.Checked = !balance.Checked;

            balances[e.Position] = balance;
            cards.Adapter = new SingleListViewAdapter(this, balances);
        }


        private List<Balance> GetBalances(string text)
        {
            List<Balance> result = new List<Balance>();

            JArray array = JArray.Parse(text);

            foreach (var item in array)
            {
                var id = item.Value<int>("ID");
                var cash = item.Value<decimal>("Cash");
                result.Add(new Balance { ID = id, Cash = cash, Checked = false});
            }

            return result;
        }

        private void ProductsInit()
        {
            purchases = FindViewById<ListView>(Resource.Id.purchasesListView);

            products = new List<Product>()
            {
                new Product { ID = 0, Name = "SNICKERS", Price = 22.30m },
                new Product { ID = 0, Name = "GRANDI", Price = 8.10m },
                new Product { ID = 0, Name = "LION", Price = 12.70m },
                new Product { ID = 0, Name = "NUTS", Price = 10.50m },
                new Product { ID = 0, Name = "BOUNTY", Price = 15.00m }
            };

            MultipleListViewAdapter adapter = new MultipleListViewAdapter(this, products);

            purchases.Adapter = adapter;
            purchases.ItemClick += ProductsHandler;
        }

        private void ProductsHandler(object sender, AdapterView.ItemClickEventArgs e)
        {
            var product = products[e.Position];
            product.Checked = !product.Checked;
            
            if (product.Checked)
                sum += products[e.Position].Price;
            else
                sum -= products[e.Position].Price;

            total.Text = sum.ToString("C");

            products[e.Position] = product;
            purchases.Adapter = new MultipleListViewAdapter(this, products);
        }
    }
}