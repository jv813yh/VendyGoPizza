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
        await Shell.Current.GoToAsync("..", true);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);

        // Set the status bar color to the default color of app
        Behaviors.Add(new CommunityToolkit.Maui.Behaviors.StatusBarBehavior
        {
           StatusBarColor = Colors.DarkGoldenrod,
           StatusBarStyle = CommunityToolkit.Maui.Core.StatusBarStyle.LightContent
        });
    }
}