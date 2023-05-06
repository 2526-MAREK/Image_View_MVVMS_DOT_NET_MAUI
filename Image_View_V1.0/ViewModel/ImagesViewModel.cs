/**
* @file  ImagesViewModel.cs
* @brief Contains the ImagesViewModel class, which represents the view model for the Images page.
*/

using Image_View_V1._0.Services;
using Image_View_V1._0.Helpers;

namespace Image_View_V1._0.ViewModel;

/**
* @brief The view model for the Images page.
*/

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

        //Wywoływanie chunków na dwa sposoby:
        //1 sposob
        (image.ChIHDR, image.ChIHDRJson) = await imageHelper.LoadChunkData(imageService.GetChunkIHDR);

        (image.ChgAMA, image.ChgAMAJson) = await imageHelper.LoadChunkData(imageService.GetChunkgAMA);

        //2 sposob
        /*image.ChhIST = await imageService.GetChunkhIST();
        image.ChhISTJson = imageHelper.SerializeChunk<ChunkhIST>(image.ChhIST);*/
        (image.ChhIST, image.ChhISTJson) = await imageHelper.LoadChunkData<ChunkhIST>(() => imageService.GetChunkDataByName<ChunkhIST>("hIST"));

        image.ChiTXt = await imageService.GetChunkiTXt();
        image.ChiTXtJson = imageHelper.SerializeChunk<ChunkiTXt>(image.ChiTXt);

        image.ChoFFs = await imageService.GetChunkoFFs();
        image.ChoFFsJson = imageHelper.SerializeChunk<ChunkoFFs>(image.ChoFFs);

        image.ChpHYs = await imageService.GetChunkpHYs();
        image.ChpHYsJson = imageHelper.SerializeChunk<ChunkpHYs>(image.ChpHYs);

        image.ChsBIT = await imageService.GetChunksBIT();
        image.ChsBITJson = imageHelper.SerializeChunk<ChunksBIT>(image.ChsBIT);

        image.ChsPLT = await imageService.GetChunksPLT();
        image.ChsPLTJson = imageHelper.SerializeChunk<ChunksPLT>(image.ChsPLT);

        (image.ChsRGB, image.ChsRGBJson) = await imageHelper.LoadChunkData(imageService.GetChunksRGB);

        (image.ChsTER, image.ChsTERJson) = await imageHelper.LoadChunkData<ChunksTER>(() => imageService.GetChunkDataByName<ChunksTER>("sTER"));

        (image.ChtEXt, image.ChtEXtJson) = await imageHelper.LoadChunkData(imageService.GetChunktEXt);

        (image.ChtIME, image.ChtIMEJson) = await imageHelper.LoadChunkData(imageService.GetChunktIME);
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
