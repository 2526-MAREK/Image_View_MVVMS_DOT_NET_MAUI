namespace Image_View_V1._0.View.ChunkSection;

public partial class IHDRSection : ContentView
{
	public IHDRSection(ImageDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}