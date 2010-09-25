using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace RestGuide
{
	public class Application
    {
        static void Main (string[] args)
        {
			try
			{
            		UIApplication.Main (args, null, "AppController");
			}
			catch (Exception ex)
			{	// HACK: this is just here for debugging
				Console.WriteLine(ex);
			}
        }
    }
}
