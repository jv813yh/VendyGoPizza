namespace VendyGoPizza.MAUI.Views;

public partial class CartPage : ContentPage
{
	private readonly CartViewModel _cartViewModel;
    public CartPage(CartViewModel cartViewModel)
	{
		InitializeComponent();
        _cartViewModel = cartViewModel;

        // Set the binding context to the view model
        BindingContext = _cartViewModel;
    }
}