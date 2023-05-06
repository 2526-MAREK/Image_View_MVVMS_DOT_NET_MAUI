/**
* @file IHDRSection.xaml.cs
* @brief Contains the IHDRSection class, which represents the IHDR section of the PNG file.
*/

namespace Image_View_V1._0.View.ChunkSection;

/**
* @brief Interaction logic for IHDRSection.xaml
*/

public partial class IHDRSection : ContentView
{
	public IHDRSection(ImageDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}