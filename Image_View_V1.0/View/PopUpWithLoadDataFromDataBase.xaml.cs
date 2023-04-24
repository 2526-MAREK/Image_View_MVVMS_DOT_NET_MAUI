using Mopups.Pages;
using Mopups.Services;

namespace Image_View_V1._0;

public partial class PopUpWithLoadDataFromDataBase : PopupPage
{
	/*public PopUpWithLoadDataFromDataBases()//(PopUpWithLoadDataFromDataBaseViewModel popUpWithLoadDataFromDataBaseViewModel)
	{
		InitializeComponent();
        //BindingContext = new PopUpWithLoadDataFromDataBaseViewModel(Navigation);

        //BindingContext = popUpWithLoadDataFromDataBaseViewModel;
    }*/

    public PopUpWithLoadDataFromDataBase()
    {
        InitializeComponent();

    }
    private void CloseButtonClick(object sender, EventArgs e)
    {
        //this.Navigation.PopModalAsync();
        MopupService.Instance.PopAsync();
    }
}