using Image_View_V1._0.Services;
using Image_View_V1._0.Helpers;

namespace Image_View_V1._0.ViewModel;

public partial class ImagesViewModel : BaseViewModel
{
    ImageService imageService;
    ImageToProcessHelper imageHelper;
    public ImagesViewModel(ImageService imageService, ImageToProcessHelper imageHelper)
    {
        this.imageService = imageService;
        this.imageHelper = imageHelper;
    }

    [RelayCommand]
async Task GoToDetailsAsync(ImageToProcess image)
    {
        //uruchamiamy python....
        Debug.WriteLine("Execute python process...");
        await imageService.RunPythonToImageProcess();

        Debug.WriteLine("Skrypt w  pythonie się wykonał\n");

        image.ChIHDR = await imageService.GetChunkIHDR();
        image.ChIHDRJson = imageHelper.SerializeChunkIHDR(image.ChIHDR);

        //var tempImage = image.ImageSrcMain;
        //image.ImageSrcMainBytes = await imageHelper.ImageSourceToByteArrayAsync(tempImage);

        image.ImageSrcFFT = imageService.GetImageFFTFromProcess();
        image.ImageSrcFFTBytes = await imageHelper.ImageSourceToByteArrayAsync(imageService.GetImageFFTFromProcess());

        image.ImageSrcMiniature = imageService.GetImageMiniatureFromProcess();
        image.ImageSrcMiniatureBytes = await imageHelper.ImageSourceToByteArrayAsync(imageService.GetImageMiniatureFromProcess());


        image.ImageSrcHist = imageService.GetImageHistFromProcess();
        image.ImageSrcHistBytes = await imageHelper.ImageSourceToByteArrayAsync(imageService.GetImageHistFromProcess());

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, 
            new Dictionary<string, object>
            {
                {"ImageToProcess", image },
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
