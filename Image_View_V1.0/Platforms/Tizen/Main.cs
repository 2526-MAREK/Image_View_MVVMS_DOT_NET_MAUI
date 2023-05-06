/**
* @file Main.cs
* @brief Contains the Program class, which is the entry point of the application.
*/

using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Image_View_V1._0;

class Program : MauiApplication
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	static void Main(string[] args)
	{
		var app = new Program();
		app.Run(args);
	}
}
