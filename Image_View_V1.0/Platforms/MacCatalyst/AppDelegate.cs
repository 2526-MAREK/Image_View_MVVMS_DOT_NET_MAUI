/**
* @file	AppDelegate.cs
* @brief	Contains the AppDelegate class, which is the entry point for the Mac Catalyst app.
*/

using Foundation;

namespace Image_View_V1._0;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
