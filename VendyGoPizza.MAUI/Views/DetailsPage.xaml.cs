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
}