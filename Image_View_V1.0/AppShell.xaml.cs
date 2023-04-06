using Image_View_V1._0;

namespace Image_View_V1._0;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        //nameof(DetailsPage) == "DetailsPage"
        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
    }
}