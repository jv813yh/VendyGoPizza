namespace VendyGoPizza.MAUI.Views;

public partial class CheckoutPage : ContentPage
{
	public CheckoutPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // little animation of elements
        checkedImage.ScaleTo(1.5);
        messageLabel.ScaleTo(1);

        await checkedImage.ScaleTo(0.5);
        await checkedImage.ScaleTo(1.5);
        await checkedImage.ScaleTo(0.5);
        await checkedImage.ScaleTo(1.5);
        await checkedImage.ScaleTo(0.5);
        await checkedImage.ScaleTo(1.5);
        await checkedImage.ScaleTo(1);

        goToHomeButton.FadeTo(1, 500);
        await goToHomeButton.ScaleTo(1);
    }

    private async void GoToHomeButtonEventHandler(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }
}