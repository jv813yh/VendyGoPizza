namespace VendyGoPizza.MAUI.Views;

public partial class DetailsPage : ContentPage
{
	private readonly DetailsPageViewModel _detailsPageViewModel;
	public DetailsPage(DetailsPageViewModel detailsPageViewModel)
	{
		InitializeComponent();
        _detailsPageViewModel = detailsPageViewModel;

        BindingContext = _detailsPageViewModel;
    }

    private async void ImageButtonClickedHandler(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopToRootAsync();
    }
}