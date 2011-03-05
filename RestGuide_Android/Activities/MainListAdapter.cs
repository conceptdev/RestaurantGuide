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

namespace RestGuide
{
    public class MainListAdapter : BaseAdapter
    {
        private IEnumerable<Restaurant> _restaurants;
        private Activity _context;
        private bool _showCount;

        /// <summary>
        /// Show Restaurant cells
        /// </summary>
        public MainListAdapter(Activity context, IEnumerable<Restaurant> restaurants)
        {
            _context = context;
            _restaurants = restaurants;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = (convertView
                            ?? _context.LayoutInflater.Inflate(
                                    Resource.Layout.MainListItem, parent, false)
                        ) as LinearLayout;
            var restaurant = _restaurants.ElementAt(position);

            view.FindViewById<TextView>(Resource.Id.Title).Text = restaurant.Name;
            view.FindViewById<TextView>(Resource.Id.Cuisine).Text = restaurant.Cuisine;

            return view;
        }

        public override int Count
        {
            get { return _restaurants.Count(); }
        }

        public Restaurant GetCategory(int position)
        {
            return _restaurants.ElementAt(position);
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
    }
}