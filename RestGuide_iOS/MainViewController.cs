using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

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
			
            public TableViewSource (List<Restaurant> list, MainViewController controller)
            {
                this.list = list;
				mvc = controller;
            }
			
			public override int RowsInSection (UITableView tableview, int section)
            {
                return list.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
                if (cell == null)
                {
                    cell = new UITableViewCell (UITableViewCellStyle.Subtitle, kCellIdentifier);
                }
                cell.TextLabel.Text = list[indexPath.Row].Name;
				cell.DetailTextLabel.Text = list[indexPath.Row].Cuisine;
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                return cell;
            }
			
			/// <summary>
			/// If there are subsections in the hierarchy, navigate to those
			/// ASSUMES there are _never_ Categories hanging off the root in the hierarchy
			/// </summary>
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
            {
                Console.WriteLine("MAIN TableViewDelegate.RowSelected: Label={0}", list[indexPath.Row].Name);
				
				var uivc = new RestaurantViewController(mvc, list[indexPath.Row]);
				uivc.Title = list[indexPath.Row].Name;
				mvc.NavigationController.PushViewController(uivc,true);
			}
		}
		
//		#region Obsolete Delegates
//		[Obsolete("Replaced by UITableViewSource")]
//        private class TableViewDelegate : UITableViewDelegate
//        {
//            private List<Restaurant> list;
//			private MainViewController mvc;
//			
//            public TableViewDelegate(List<Restaurant> list, MainViewController controller)
//            {
//                this.list = list;
//				mvc = controller;
//            }
//
//			/// <summary>
//			/// If there are subsections in the hierarchy, navigate to those
//			/// ASSUMES there are _never_ Categories hanging off the root in the hierarchy
//			/// </summary>
//			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
//            {
//                Console.WriteLine("MAIN TableViewDelegate.RowSelected: Label={0}", list[indexPath.Row].Name);
//				
//				SectionViewController uivc = new SectionViewController(mvc);
//				uivc.Title = list[indexPath.Row].Name;
//				//uivc.Sections = mvc.Restaurants[indexPath.Row].Sections;
//				if (uivc.Sections.Count == 0)
//				{
//					Console.WriteLine("Doesn't support 'words' hanging off the root RogetClass elements");
//				}
//				else
//				{
//					Console.WriteLine("  thesaurus count: " + uivc.Sections.Count);
//					mvc.NavigationController.PushViewController(uivc,true);
//            		}
//			}
//        }
//
//		[Obsolete("Replaced by UITableViewSource")]
//        private class TableViewDataSource : UITableViewDataSource
//        {
//            static NSString kCellIdentifier = new NSString ("MyIdentifier");
//            private List<Restaurant> list;
//			private MainViewController mvc;
//            public TableViewDataSource (List<Restaurant> list, MainViewController controller)
//            {
//                this.list = list;
//				mvc = controller;
//            }
//
//            public override int RowsInSection (UITableView tableview, int section)
//            {
//                return list.Count;
//            }
//
//            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
//            {
//                UITableViewCell cell = tableView.DequeueReusableCell (kCellIdentifier);
//                if (cell == null)
//                {
//                    cell = new UITableViewCell (UITableViewCellStyle.Default, kCellIdentifier);
//                }
//                cell.TextLabel.Text = list[indexPath.Row].Name;
//				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
//                return cell;
//            }
//        }
//		#endregion
    }
}