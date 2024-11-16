namespace VendyGoPizza.MAUI.ViewModels
{
    [QueryProperty(nameof(CurrentPizza), nameof(CurrentPizza))]
    public partial class DetailsPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Pizza _currentPizza;


        protected override void SetFalseBoolValues()
        {
            
        }

        protected override void SetTrueBoolValues()
        {
           
        }
    }
}
