/**
* @file MainPage.xaml.cs
* @brief Contains the MainPage class, which represents the main page of the application.
*/

namespace Image_View_V1._0.View;

public partial class MainPage : ContentPage
{
    public MainPage(ImagesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}



