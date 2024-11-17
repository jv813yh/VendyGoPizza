namespace VendyGoPizza.MAUI.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizerHandler(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}
