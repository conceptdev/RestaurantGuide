using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RestGuide
{
	/// <remarks>
	/// Uses UIWebView since we want to format the text display (with HTML)
	/// </remarks>
	public class RestaurantViewController : UIViewController
	{
		Restaurant rest;
		
		public RestaurantViewController (MainViewController mvc, Restaurant restaurant) : base()
		{
			rest = restaurant;
		}
		
		public UITextView textView;
		public UIWebView webView;
		
		public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			// no XIB !
			webView = new UIWebView()
			{
				ScalesPageToFit = false
			};
			webView.LoadHtmlString(FormatText(), new NSUrl());
			
			// Set the web view to fit the width of the app.
            webView.SizeToFit();

            // Reposition and resize the receiver
            webView.Frame = new RectangleF (
                0, 0, this.View.Bounds.Width, this.View.Bounds.Height);

            // Add the table view as a subview
            this.View.AddSubview(webView);
			
		}		
		/// <summary>
		/// Format the restaurant text for UIWebView
		/// </summary>
		private string FormatText()
		{
			StringBuilder sb = new StringBuilder();
			
			sb.Append(@"<style>
body,b,p{font-family:Helvetica;font-size:14px}
</style>");
sb.Append("<span style='font-size:28px;font-weight:bold;'>" + rest.Name + "</span>" + Environment.NewLine);
//sb.Append("<span style='float:right;size:10px;color:#555555;'>#" + rest.Number + "</span>" + Environment.NewLine);
sb.Append("<br/>" + Environment.NewLine);
sb.Append("<span style='color:#8CBF26;size:10px'><b>" + rest.Cuisine.ToUpper() + "</b></span><br/>" + Environment.NewLine);
sb.Append("<i>" + rest.Address + "</i><br/>" + Environment.NewLine);
sb.Append("<span style='color:#8CBF26;'><b>T</b></span> <span style='color:#cccccc;'>|</span> " + Environment.NewLine);
sb.Append(rest.Phone + "<br/>" + Environment.NewLine);
sb.Append("<span style='color:#8CBF26;'><b>W</b></span> <span style='color:#cccccc;'>|</span> " + Environment.NewLine);
sb.Append(
	String.Format("<a href='{0}'>{1}</a><br/>", rest.Website,rest.Website)
 + Environment.NewLine);

sb.Append("<br/>" + rest.Text + "<br/><br/>" + Environment.NewLine);

sb.Append("<div style='background-color:#8CBF26;padding:8px;'>" + Environment.NewLine);
sb.Append("<div style='size:10px'><b>HOURS</b></div>" + Environment.NewLine);
sb.Append(rest.Hours.Replace("\n","<br/>") + "<br/>" + Environment.NewLine);
sb.Append("<div style='size:10px;padding:10 0 0 0;'><b>CARD TYPES ACCEPTED</b></div>" + Environment.NewLine);
sb.Append(rest.CreditCards.Replace("\n","<br/>") + "<br/>" + Environment.NewLine);
sb.Append("<div style='size:10px;padding:10 0 0 0;'><b>CHEF</b></div>" + Environment.NewLine);
sb.Append(rest.Chef + "<br/>" + Environment.NewLine);
sb.Append("</div>" + Environment.NewLine);
sb.Append("<br/>");
sb.Append("<br/>");
			
			return sb.ToString();
		}
	}
}