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
    public class MultipleListViewAdapter : BaseAdapter<Product>
    {
        private readonly List<Product> items;
        private readonly Context context;

        public MultipleListViewAdapter(Context context, List<Product> items)
        {
            this.items = items;
            this.context = context;
        }

        public override Product this[int position] 
            => items[position];

        public override int Count 
            => items.Count;

        public override long GetItemId(int position)
            => items[position].ID;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if(row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.ListItemMultipleChoise, null, false);
            }

            var nameTextView = row.FindViewById<TextView>(Resource.Id.nameTextView);
            var priceTextView = row.FindViewById<TextView>(Resource.Id.priceTextView);
            var checkBox= row.FindViewById<CheckBox>(Resource.Id.listCheckBox);

            var item = items[position];
            nameTextView.Text = item.Name;
            priceTextView.Text = item.Price.ToString("C");
            checkBox.Checked = item.Checked;

            return row;
        }
    }

    public class SingleListViewAdapter : BaseAdapter<Balance>
    {
        private readonly List<Balance> items;
        private readonly Context context;

        public SingleListViewAdapter(Context context, List<Balance> items)
        {
            this.items = items;
            this.context = context;
        }

        public override Balance this[int position]
            => items[position];

        public override int Count
            => items.Count;

        public override long GetItemId(int position)
            => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.ListItemSingleChoise, null, false);
            }

            var balanceTextView = row.FindViewById<TextView>(Resource.Id.balanceTextView);
            var listRadioButton = row.FindViewById<RadioButton>(Resource.Id.listRadioButton);

            var item = items[position];
            balanceTextView.Text = item.Cash.ToString("C");
            listRadioButton.Checked = item.Checked;

            return row;
        }
    }
}