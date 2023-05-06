/**
* @file AppShell.xaml.cs
* @brief Contains the AppShell class, which represents the application shell.
*/

using Image_View_V1._0;

namespace Image_View_V1._0;

/**
* @brief Interaction logic for AppShell.xaml
*/

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        //nameof(DetailsPage) == "DetailsPage"
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(PopUpWithLoadDataFromDataBase), typeof(PopUpWithLoadDataFromDataBase));
    }
}