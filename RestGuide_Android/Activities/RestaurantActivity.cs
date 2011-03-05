using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;

namespace RestGuide
{
    [Activity(Label = "Restaurant Guide")]
    public class RestaurantActivity : Activity
    {
        string restaurantName;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            restaurantName = Intent.GetStringExtra("Name");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Restaurant);

            var restaurants = ((RestGuideApplication)Application).Restaurants;

            var re = from rest in restaurants
                    where rest.Name == restaurantName
                    select rest;
            var restaurant = re.FirstOrDefault();

            // Get our button from the layout resource,
            // and attach an event to it
            TextView heading = FindViewById<TextView>(Resource.Id.Title);
            TextView cuisine = FindViewById<TextView>(Resource.Id.Cuisine);
            TextView address = FindViewById<TextView>(Resource.Id.Address);
            TextView telephone = FindViewById<TextView>(Resource.Id.Telephone);
            TextView website = FindViewById<TextView>(Resource.Id.Website);
            TextView description = FindViewById<TextView>(Resource.Id.Description);

            TextView hours = FindViewById<TextView>(Resource.Id.Hours);
            TextView creditCards = FindViewById<TextView>(Resource.Id.CreditCards);
            TextView chef = FindViewById<TextView>(Resource.Id.Chef);
            
            heading.Text = restaurant.Name;
            cuisine.Text = restaurant.Cuisine.ToUpper();
            address.Text = restaurant.Address;
            telephone.Text = "T | " + restaurant.Phone;
            website.Text = "W | " + restaurant.Website;
            description.Text = restaurant.Text;

            hours.Text = restaurant.Hours;
            creditCards.Text = restaurant.CreditCards;
            chef.Text = restaurant.Chef;
        }
    }
}

