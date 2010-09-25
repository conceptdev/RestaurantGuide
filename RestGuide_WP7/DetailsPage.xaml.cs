using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Text;

namespace RestGuide
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
            Web.Opacity = 0;
            Web.Loaded += new RoutedEventHandler(Web_Loaded);
        }

        void Web_Loaded(object sender, RoutedEventArgs e)
        {
            Web.NavigateToString(FormatHtml(r));
            Web.Opacity = 1;
        }

        Restaurant r;
        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                //DataContext = App.ViewModel.Items[index];
                 r= App.ViewModel.Restaurants[index];
                 DataContext=r;
            }
        }

        string FormatHtml(Restaurant rest)
        { 
			StringBuilder sb = new StringBuilder();
			
			sb.Append(@"<style>
body{background-color:#000000;}
body,b,p{font-family:Helvetica;font-size:14px;color:#ffffff;}
</style>");
//sb.Append("<span style='font-size:28px;font-weight:bold;'>" + rest.Name + "</span>" + Environment.NewLine);
//sb.Append("<span style='float:right;size:12px;color:#555555;'>#" + rest.Number + "</span>" + Environment.NewLine);
//sb.Append("<br/>" + Environment.NewLine);
sb.Append("<span style='color:#DE7C30;size:12px'><b>" + rest.Cuisine.ToUpper() + "</b></span><br/>" + Environment.NewLine);
sb.Append("<i>" + rest.Address + "</i><br/>" + Environment.NewLine);
sb.Append("<span style='color:#DE7C30;'><b>T</b></span> <span style='color:#cccccc;'>|</span> " + Environment.NewLine);
sb.Append(rest.Phone + "<br/>" + Environment.NewLine);
sb.Append("<span style='color:#DE7C30;'><b>W</b></span> <span style='color:#cccccc;'>|</span> " + Environment.NewLine);
sb.Append(
	String.Format("<a href='{0}'>{1}</a><br/>", rest.Website,rest.Website)
 + Environment.NewLine);

sb.Append("<br/>" + rest.Text + "<br/><br/>" + Environment.NewLine);

sb.Append("<div style='background-color:#8CBF26;padding:8px;'>" + Environment.NewLine);
sb.Append("<div style='background-color:#8CBF26;size:12px'><b>HOURS</b></div>" + Environment.NewLine);
sb.Append(rest.Hours.Replace("\n","<br/>") + "<br/>" + Environment.NewLine);
sb.Append("<div style='background-color:#8CBF26;size:12px;padding:10 0 0 0;'><b>CARD TYPES ACCEPTED</b></div>" + Environment.NewLine);
sb.Append(rest.CreditCards.Replace("\n","<br/>") + "<br/>" + Environment.NewLine);
sb.Append("<div style='background-color:#8CBF26;size:12px;padding:10 0 0 0;'><b>CHEF</b></div>" + Environment.NewLine);
sb.Append(rest.Chef + "<br/>" + Environment.NewLine);
sb.Append("</div>" + Environment.NewLine);
sb.Append("<br/>");
sb.Append("<br/>");

			
			return sb.ToString();
		}
    }
}