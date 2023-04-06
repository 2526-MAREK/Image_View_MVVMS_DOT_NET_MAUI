namespace Image_View_V1._0;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(ImageDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}