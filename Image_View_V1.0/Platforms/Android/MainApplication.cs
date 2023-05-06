/**
* @file MainApplication.cs
* @brief Contains the MainApplication class, which represents the Android application.
*/

using Android.App;
using Android.Runtime;

namespace Image_View_V1._0;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
