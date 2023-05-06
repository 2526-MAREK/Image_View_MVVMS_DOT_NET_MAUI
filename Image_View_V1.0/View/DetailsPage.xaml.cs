/**
* @file     DetailsPage.xaml.cs
* @brief    Contains the DetailsPage class, which represents the DetailsPage of the application.    
*/

using Mopups.Interfaces;
using Mopups.Services;

namespace Image_View_V1._0;

/**
* @brief Interaction logic for DetailsPage.xaml
*/

public partial class DetailsPage : ContentPage
{
    
    public DetailsPage(ImageDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        //this.popupNavigation = popupNavigation;
       
    }
}