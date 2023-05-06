/**
* @file BaseViewModel.cs
* @brief Contains the BaseViewModel class, which is the base class for all view models.
*/

namespace Image_View_V1._0.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;
}

