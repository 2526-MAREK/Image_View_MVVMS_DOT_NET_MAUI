//using MvvmHelpers;
//using MvvmHelpers.Commands;
//using Command = MvvmHelpers.Commands.Command;
using Image_View_V1._0.Services;
using Mopups.Services;
using Mopups.Interfaces;

namespace Image_View_V1._0.ViewModel;

[QueryProperty("ImageToProcess", "ImageToProcess")]
public partial class ImageDetailsViewModel : BaseViewModel
{
    ImageDataBaseService imageDataBaseService;
    IPopupNavigation popupNavigation;

    [ObservableProperty]
    ImageToProcess imageToProcess;

    /*public AsyncCommand RefreshCommand { get; }
    public AsyncCommand AddCommand { get; }
    public AsyncCommand<ImageToProcess> RemoveCommand { get; }
    public AsyncCommand<ImageToProcess> SelectedCommand { get; }*/

    public ImageDetailsViewModel(ImageDataBaseService imageDataBaseService, IPopupNavigation popupNavigation)
    {
        this.imageDataBaseService = imageDataBaseService;
        this.popupNavigation = popupNavigation;
        
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

    [RelayCommand]
    async Task RemoveImageAfterProcessFromDataBase(ImageToProcess imageToProcess)
    {
        await imageDataBaseService.RemoveImageAfterProcessFromDataBase(imageToProcess.Id);
        //await Refresh();
    }

    [RelayCommand]
    async Task LoadImageAfterProcessFromDataBase(ImageToProcess imageToProcess)
    { 
        imageToProcess = await imageDataBaseService.GetImageAfterProcessFromDataBase(imageToProcess.Id);
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
    async Task ShowPopUpWithLoadDataFromDataBase()
    {
        popupNavigation.PushAsync(new PopUpWithLoadDataFromDataBase());
    }

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

 }
