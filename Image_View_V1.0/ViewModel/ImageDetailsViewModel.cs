/**
* @file ImageDetailsViewModel.cs
* @brief Contains the ImageDetailsViewModel class, which is the view model for the ImageDetailsPage.
*/

//using MvvmHelpers;
//using MvvmHelpers.Commands;
//using Command = MvvmHelpers.Commands.Command;
using Image_View_V1._0.Services;
using Image_View_V1._0.Helpers;
using Mopups.Services;
using Mopups.Interfaces;

namespace Image_View_V1._0.ViewModel;

[QueryProperty("ImageToProcess", "ImageToProcess")]
/**
* @brief The view model for the ImageDetailsPage.
*/

public partial class ImageDetailsViewModel : BaseViewModel
{
    ImageDataBaseService imageDataBaseService;
    IPopupNavigation popupNavigation;
    ImageToProcessHelper imageHelper;

    [ObservableProperty]
    ImageToProcess imageToProcess;

    /*public AsyncCommand RefreshCommand { get; }
    public AsyncCommand AddCommand { get; }
    public AsyncCommand<ImageToProcess> RemoveCommand { get; }
    public AsyncCommand<ImageToProcess> SelectedCommand { get; }*/

    public ImageDetailsViewModel(ImageDataBaseService imageDataBaseService, IPopupNavigation popupNavigation, ImageToProcessHelper imageHelper)
    {
        this.imageDataBaseService = imageDataBaseService;
        this.popupNavigation = popupNavigation;
        this.imageHelper = imageHelper;
        
        //RefreshCommand = new AsyncCommand(Refresh);
        /*AddCommand = new AsyncCommand(Add);
        RemoveCommand = new AsyncCommand<ImageToProcess>(Remove);
        SelectedCommand = new AsyncCommand<ImageToProcess>(Selected);*/

        //imageDataBaseService = DependencyService.Get<IImageDataBaseService>();
    }

    [RelayCommand]
    async Task AddImageAfterProcessToDataBase()
    {
        imageToProcess.NameOfImageToDataBase = await App.Current.MainPage.DisplayPromptAsync("Name", "Name of Image After Processed");
        await imageDataBaseService.AddImageAfterProcessToDataBase(imageToProcess);  
       // await Refresh();

       /* var route = $"{nameof(AddMyCoffeePage)}?Name=Motz";
        await Shell.Current.GoToAsync(route);*/

    }

    /*async Task Selected(Coffee coffee)
    {
        if (coffee == null)
            return;

        var route = $"{nameof(MyCoffeeDetailsPage)}?CoffeeId={coffee.Id}";
        await Shell.Current.GoToAsync(route);
    }*/

    private async void RemoveImageAfterProcessFromDataBase(object sender, int e)
    {
        await imageDataBaseService.RemoveImageAfterProcessFromDataBase(e);

        //await Refresh();
    }

    
    private async void LoadImageAfterProcessFromDataBase(object sender, int e)
    {
        ImageToProcess imageToProcessTemp;
        imageToProcessTemp = await imageDataBaseService.GetImageAfterProcessFromDataBase(e);

        imageToProcessTemp.ChIHDR = imageHelper.DeserializeChunk< ChunkIHDR>(imageToProcessTemp.ChIHDRJson);
        imageToProcessTemp.ChgAMA = imageHelper.DeserializeChunk<ChunkgAMA>(imageToProcessTemp.ChgAMAJson);
        imageToProcessTemp.ChhIST = imageHelper.DeserializeChunk<ChunkhIST>(imageToProcessTemp.ChhISTJson);
        imageToProcessTemp.ChiTXt = imageHelper.DeserializeChunk<ChunkiTXt>(imageToProcessTemp.ChiTXtJson);
        imageToProcessTemp.ChoFFs = imageHelper.DeserializeChunk<ChunkoFFs>(imageToProcessTemp.ChoFFsJson);
        imageToProcessTemp.ChpHYs = imageHelper.DeserializeChunk<ChunkpHYs>(imageToProcessTemp.ChpHYsJson);
        imageToProcessTemp.ChsBIT = imageHelper.DeserializeChunk<ChunksBIT>(imageToProcessTemp.ChsBITJson);
        imageToProcessTemp.ChsPLT = imageHelper.DeserializeChunk<ChunksPLT>(imageToProcessTemp.ChsPLTJson);
        imageToProcessTemp.ChsRGB = imageHelper.DeserializeChunk<ChunksRGB>(imageToProcessTemp.ChsRGBJson);
        imageToProcessTemp.ChsTER = imageHelper.DeserializeChunk<ChunksTER>(imageToProcessTemp.ChsTERJson);
        imageToProcessTemp.ChtEXt = imageHelper.DeserializeChunk<ChunktEXt>(imageToProcessTemp.ChtEXtJson);
        imageToProcessTemp.ChtIME = imageHelper.DeserializeChunk<ChunktIME>(imageToProcessTemp.ChtIMEJson);


        imageToProcessTemp.ImageSrcMain = imageHelper.ByteArrayToImageSource(imageToProcessTemp.ImageSrcMainBytes);
        imageToProcessTemp.ImageSrcFFT = imageHelper.ByteArrayToImageSource(imageToProcessTemp.ImageSrcFFTBytes);
        imageToProcessTemp.ImageSrcHist = imageHelper.ByteArrayToImageSource(imageToProcessTemp.ImageSrcHistBytes);
        imageToProcessTemp.ImageSrcMiniature = imageHelper.ByteArrayToImageSource(imageToProcessTemp.ImageSrcMiniatureBytes);

        ImageToProcess = imageToProcessTemp;
    }

    [RelayCommand]
    async Task LoadAllImageAfterProcessFromDataBase()
    {
        IEnumerable<ImageToProcess> listOfImageAfterProcess;
        listOfImageAfterProcess = await imageDataBaseService.GetAllImageAfterProcessFromDataBase();
    }

    /*async Task Refresh()
    {
        IsBusy = true;

#if DEBUG
        await Task.Delay(500);
#endif

        ImageToProcess.Clear();

        var coffees = await coffeeService.GetCoffee();

        Coffee.AddRange(coffees);

        IsBusy = false;

        DependencyService.Get<IToast>()?.MakeToast("Refreshed!");
    }*/

    [RelayCommand]
    async Task ShowPopUpWithDeleteDataFromDataBase()
    {
        IEnumerable<ImageToProcess> listOfImagesAfterProcess = await imageDataBaseService.GetAllImageAfterProcessFromDataBase();
        var popupPage = new PopUpWithLoadDataFromDataBase(listOfImagesAfterProcess); // Zastąp nazwą swojej klasy popup
        popupPage.ButtonClicked += RemoveImageAfterProcessFromDataBase;
        //await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popupPage);

        await popupNavigation.PushAsync(popupPage);
        
    }

    [RelayCommand]
    async Task ShowPopUpWithLoadDataFromDataBase()
    {
        IEnumerable<ImageToProcess> listOfImagesAfterProcess = await imageDataBaseService.GetAllImageAfterProcessFromDataBase();
        var popupPage = new PopUpWithLoadDataFromDataBase(listOfImagesAfterProcess); // Zastąp nazwą swojej klasy popup
        popupPage.ButtonClicked += LoadImageAfterProcessFromDataBase;
        //await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popupPage);

        await popupNavigation.PushAsync(popupPage);
    }

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

 }
