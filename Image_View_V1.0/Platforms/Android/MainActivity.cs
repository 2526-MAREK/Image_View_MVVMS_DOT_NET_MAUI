/**
* @file MainActivity.cs
* @brief Contains the MainActivity class, which is the entry point for the Android application.
*/

using Android.App;
using Android.Content.PM;
using Android.OS;

namespace Image_View_V1._0;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}
