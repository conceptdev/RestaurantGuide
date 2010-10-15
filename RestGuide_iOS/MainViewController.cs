using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Linq;

namespace RestGuide
{
	/// <summary>
	/// First view that users see - lists the top level of the hierarchy xml
	/// </summary>
	/// <remarks>
	/// LOADS data from the xml files into public properties (deserialization)
	/// then we pass around references to the MainViewController so other
	/// ViewControllers can access the data.
	/// 
	/// Added the new UITableViewSource</remarks>
    [Register]
    public class MainViewController : UITableViewController
    {
		/// <summary>The main data-set of words by part-of-speech</summary>
		public List<Restaurant> Restaurants;

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

			#region load data from XML
			using (TextReader reader = new StreamReader("restaurants.xml"))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<Restaurant>));
				Restaurants = (List<Restaurant>)serializer.Deserialize(reader);
			}
			#endregion
			TableView.Source = new TableViewSource (Restaurants, this);
			TableView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight|
			                       UIViewAutoresizing.FlexibleWidth;
			TableView.BackgroundColor = UIColor.White;
			
            Console.WriteLine("Is you're using the simulator, switch to it now.");
        }

		/// <summary>
		/// Extends the new UITableViewSource in MonoTouch 1.2 (4-Nov-09)
		/// </summary>
		private class TableViewSource : UITableViewSource
		{
            static NSString kCellIdentifier = new NSString ("MyIdentifier");
			private List<Restaurant> list;
			private MainViewController mvc;
			
			List<string> sectionTitles;
			Dictionary<int, List<Restaurant>> sectionElements = new Dictionary<int, List<Restaurant>>();
            public TableViewSource (List<Restaurant> list, MainViewController controller)
            {
                this.list = list;
				mvc = controller;
				sectionTitles = (from r in list
									orderby r.StartsWith
									select r.StartsWith).Distinct().ToList();
				foreach (var restaurant in list)
				{	// group elements together into 'alphabet'
					int sectionNumber = sectionTitles.IndexOf(restaurant.Name[0].ToString());
					if (sectionElements.ContainsKey(sectionNumber))
						sectionElements[sectionNumber].Add(restaurant);
					else
						sectionElements.Add(sectionNumber, new List<Restaurant> {restaurant});
				}
            }
			public override int NumberOfSections (UITableView tableView)
			{
				return sectionTitles.Count;
			}
			public override string TitleForHeader (UITableView tableView, int section)
			{
				return sectionTitles[section];
			}
			public override string[] SectionIndexTitles (UITableView tableView)
			{
				return sectionTitles.ToArray();
			}
			public override int RowsInSection (UITableView tableview, int section)
            {
                return sectionElements[section].Count(); //list.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
                if (cell == null)
                {
                    cell = new UITableViewCell (UITableViewCellStyle.Subtitle, kCellIdentifier);
                }
                cell.TextLabel.Text = sectionElements[indexPath.Section][indexPath.Row].Name;
				cell.DetailTextLabel.Text = sectionElements[indexPath.Section][indexPath.Row].Cuisine;

                return cell;
            }
			
			/// <summary>
			/// If there are subsections in the hierarchy, navigate to those
			/// ASSUMES there are _never_ Categories hanging off the root in the hierarchy
			/// </summary>
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
                Console.WriteLine("MAIN TableViewDelegate.RowSelected: Label={0}", list[indexPath.Row].Name);
				
				var uivc = new RestaurantViewController(mvc, sectionElements[indexPath.Section][indexPath.Row]);
				uivc.Title = sectionElements[indexPath.Section][indexPath.Row].Name;
				mvc.NavigationController.PushViewController(uivc,true);
			}
		}
    }
}