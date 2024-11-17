using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;

namespace VendyGoPizza.MAUI.Views;

public partial class AllPizzasPage : ContentPage
{
	private readonly AllPizzasViewModel _allPizzasViewModel;

    public AllPizzasPage(AllPizzasViewModel allPizzasViewModel)
	{
		InitializeComponent();
        _allPizzasViewModel = allPizzasViewModel;

        // Set the binding context to the view model
        BindingContext = _allPizzasViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if(_allPizzasViewModel.IsFromSearch == "True")
        {
            // Must delay for the search bar to be rendered
            await Task.Delay(100);
            searchBarForPizza.Focus();
        }
    }

    private void SearchBarTextChangedHandler(object sender, TextChangedEventArgs e)
    {
        if(e.OldTextValue != null && string.IsNullOrEmpty(e.NewTextValue))
        {
            _allPizzasViewModel.SearchPizzaCommand.Execute(null);
        }
    }
}