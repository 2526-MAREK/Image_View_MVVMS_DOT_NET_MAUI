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
async Task GoToDetailsAsync(ImageSource image)
    {
        ImageToProcess imgSrcTemp = new();

        imgSrcTemp.ImageSrc = image;

        //uruchamiamy python....
        
        imgSrcTemp.ChIHDR = await imageService.GetChunkIHDR();

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, 
            new Dictionary<string, object>
            {
                {"ImageToProcess", imgSrcTemp }
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

            var image = await imageService.GetImage();

            if (image != null)
            {
                await GoToDetailsAsync(image);
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
