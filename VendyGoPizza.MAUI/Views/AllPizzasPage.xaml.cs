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
}