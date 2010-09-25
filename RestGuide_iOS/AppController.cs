using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RestGuide
{
	/// <summary>
	/// ROOT of this application; referenced in "Main.cs"
	/// </summary>
	[Register ("AppController")]
    public class AppController : UIApplicationDelegate
    {
        UIWindow window;
		
		MonoTouch.UIKit.UINavigationController navigationController;

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            // Create the main view controller - the 'first' view in the app
            var vc = new MainViewController ();
			
			// Create a navigation controller, to which we'll add the view
			navigationController = new UINavigationController();
			navigationController.PushViewController(vc, false);
			navigationController.TopViewController.Title ="RestGuide";
			navigationController.NavigationBar.TintColor = UIColor.FromRGB(140/255f, 191/255f, 38/255f);
			navigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			
            // Create the main window and add the navigation controller as a subview
            window = new UIWindow (UIScreen.MainScreen.Bounds);
            window.AddSubview(navigationController.View);
            window.MakeKeyAndVisible ();
            return true;
        }

        // This method is allegedly required in iPhoneOS 3.0
        public override void OnActivated (UIApplication application)
        {
        }
    }
}