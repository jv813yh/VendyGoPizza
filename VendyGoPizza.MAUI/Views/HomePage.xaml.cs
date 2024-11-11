namespace VendyGoPizza.MAUI.Views;

public partial class HomePage : ContentPage
{
	private readonly HomeViewModel _homeViewModel;

    public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
        _homeViewModel = homeViewModel;

        // Set the binding context to the view model
        BindingContext = _homeViewModel;
    }
}