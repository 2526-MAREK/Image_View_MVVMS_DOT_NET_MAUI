/**
* @file PopUpWithLoadDataFromDataBase.xaml.cs
* @brief Contains the PopUpWithLoadDataFromDataBase class, which represents the PopUpWithLoadDataFromDataBase section of the PNG file.
*/

using Mopups.Pages;
using Mopups.Services;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Image_View_V1._0;

public partial class PopUpWithLoadDataFromDataBase : PopupPage
{
    /*public PopUpWithLoadDataFromDataBases()//(PopUpWithLoadDataFromDataBaseViewModel popUpWithLoadDataFromDataBaseViewModel)
	{
		InitializeComponent();
        //BindingContext = new PopUpWithLoadDataFromDataBaseViewModel(Navigation);

        //BindingContext = popUpWithLoadDataFromDataBaseViewModel;
    }*/

    //PopupLoadDataBaseViewModel _viewModel;
    public event EventHandler<int> ButtonClicked;
    ObservableCollection<ImageToProcess> imageAfterProcessList;


    //ImageToProcess imageAfterProcess;
    public PopUpWithLoadDataFromDataBase(IEnumerable<ImageToProcess> imageAfterProcessList)
    {
        InitializeComponent();
        //_viewModel = new PopupLoadDataBaseViewModel(imageAfterProcessList);
        //BindingContext = _viewModel;
        this.imageAfterProcessList = new ObservableCollection<ImageToProcess>(imageAfterProcessList);
       // LoadButtons();
        //MyCollectionView.ItemsSource = this.imageAfterProcessList;
        // MyCollectionView.SelectionChanged     = OnNameOfChooseLoadImageClicked();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CreateButtons();
    }

    private void CreateButtons()
    {
        int rowIndex = 0;
        foreach (var item in imageAfterProcessList)
        {
            var button = new Button
            {
                Text = item.NameOfImageToDataBase, // Replace 'NameOfImageToDataBase' with the property you want to display
                Command = new Command(async () => await OnNameOfChooseLoadImageClicked(item))
            };
            Grid.SetRow(button, rowIndex);
            ButtonGrid.Children.Add(button);
            rowIndex++;
        }
        UpdateButtonVisibility();
    }

    /*private void LoadButtons()
    {
        // Pobierz dane z odpowiedniego �r�d�a
        // Wype�nij list� przycisk�w, u�ywaj�c poni�szego kodu

        ButtonGrid.RowDefinitions.Clear();
        int numberOfRows = imageAfterProcessList.Count;
        for (int i = 0; i < numberOfRows; i++)
        {
            ButtonGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        int rowIndex = 0;
        foreach (var image in imageAfterProcessList)
        {
            Button button = new Button
            {
                Text = image.NameOfImageToDataBase,
            };

            button.Clicked += OnNameOfChooseLoadImageClicked;

            Grid.SetRow(button, rowIndex);
            ButtonGrid.Children.Add(button);
            rowIndex++;
        }

        UpdateButtonVisibility();
    }*/


    private async Task OnNameOfChooseLoadImageClicked(ImageToProcess image)
    {
        //var button = sender as Button;
       // var image = button.BindingContext as ImageToProcess;
        if (image != null) { 
        
            // Zr�b co� z warto�ciami 'imageName' i 'imageType', np. wy�wietl alert
            ButtonClicked?.Invoke(this, image.Id);
        }
        MopupService.Instance.PopAsync();
    }

        private void CloseButtonClick(object sender, EventArgs e)
    {
        //this.Navigation.PopModalAsync();
        MopupService.Instance.PopAsync();
    }

    private int currentRow = 0;

    private void ScrollUpButtonClicked(object sender, EventArgs e)
    {
        if (currentRow > 0)
        {
            currentRow--;
            UpdateButtonVisibility();
        }
    }

    private void ScrollDownButtonClicked(object sender, EventArgs e)
    {
        if (currentRow < ButtonGrid.RowDefinitions.Count - 1)
        {
            currentRow++;
            UpdateButtonVisibility();
        }
    }

    private void UpdateButtonVisibility()
    {
        for (int i = 0; i < ButtonGrid.Children.Count; i++)
        {
            if (ButtonGrid.Children[i] is Microsoft.Maui.Controls.View view)
            {
                if (i >= currentRow && i < currentRow + 6)
                {
                    view.IsVisible = true;
                    Grid.SetRow(view, i - currentRow);
                }
                else
                {
                    view.IsVisible = false;
                }
            }
        }
    }
    /*public PopUpWithLoadDataFromDataBase(ImageDetailsViewModel viewModel)
    {
        InitializeComponent();
        //_viewModel = new PopupLoadDataBaseViewModel(imageAfterProcessList);
        BindingContext = viewModel;

    }*/

}