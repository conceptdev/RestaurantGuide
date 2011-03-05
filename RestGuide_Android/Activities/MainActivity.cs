using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;

namespace RestGuide
{
    [Activity(Label = "Restaurant Guide", MainLauncher = true, Icon="@drawable/icon")]
    public class MainActivity : Activity
    {
        private ListView _list;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _list = FindViewById<ListView>(Resource.Id.List);
            _list.ItemClick += new System.EventHandler<ItemEventArgs>(_list_ItemClick);


            // Get our button from the layout resource,
            // and attach an event to it
            TextView heading = FindViewById<TextView>(Resource.Id.Title);
            heading.Text = "Top 100";
        }

        protected override void OnResume()
        {
            base.OnResume();
            refreshRestaurants();
        }

        private void refreshRestaurants()
        {
            var restaurants = ((RestGuideApplication)Application).Restaurants;
            _list.Adapter = new MainListAdapter(this, restaurants);
        }

        private void _list_ItemClick(object sender, ItemEventArgs e)
        {
            System.Console.WriteLine("[MainActivity] _list_ItemClick " + e.Position);

            var restaurant = ((MainListAdapter)_list.Adapter).GetCategory(e.Position);

            var intent = new Intent();

            intent.SetClass(this, typeof(RestaurantActivity));
            intent.PutExtra("Name", restaurant.Name);
            
            StartActivity(intent);
        }
    }
}

