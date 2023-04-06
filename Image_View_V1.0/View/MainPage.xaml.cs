namespace Image_View_V1._0.View;

public partial class MainPage : ContentPage
{
    public MainPage(ImagesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}



