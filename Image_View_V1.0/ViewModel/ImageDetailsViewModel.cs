namespace Image_View_V1._0.ViewModel;

[QueryProperty("ImageToProcess", "ImageToProcess")]
public partial class ImageDetailsViewModel : BaseViewModel
{

    public  ImageDetailsViewModel()
    {
    }

    [ObservableProperty]
    ImageToProcess imageToProcess;


    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

 }
