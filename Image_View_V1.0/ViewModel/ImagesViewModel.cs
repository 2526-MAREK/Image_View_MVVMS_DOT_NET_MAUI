using Image_View_V1._0.Services;

namespace Image_View_V1._0.ViewModel;

public partial class ImagesViewModel : BaseViewModel
{
    ImageService imageService;
    public ImagesViewModel(ImageService imageService)
    {
        this.imageService = imageService;
    }

    [RelayCommand]
async Task GoToDetailsAsync(ImageToProcess image)
    {
        //uruchamiamy python....
        Debug.WriteLine("Execute python process...");
        imageService.RunPythonToImageProcess();

        Debug.WriteLine("Skrypt w  pythonie się wykonał\n");

        image.ChIHDR = await imageService.GetChunkIHDR();
        image.ImageSrcFFT = imageService.GetImageFFTFromProcess();
        image.ImageSrcMiniature = imageService.GetImageMiniatureFromProcess();

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, 
            new Dictionary<string, object>
            {
                {"ImageToProcess", image }
            });
    }

    [RelayCommand]
    async Task GetImageAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            ImageToProcess imgSrcTemp = new();
            imgSrcTemp = await imageService.GetImage();

            if (imgSrcTemp != null)
            {
                await GoToDetailsAsync(imgSrcTemp);
            }
          
}
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get image: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }

    }
}
