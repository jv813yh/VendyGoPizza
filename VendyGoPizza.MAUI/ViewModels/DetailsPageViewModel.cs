
using System;
using System.Windows.Input;

namespace VendyGoPizza.MAUI.ViewModels
{
    [QueryProperty(nameof(CurrentPizza), nameof(CurrentPizza))]
    public partial class DetailsPageViewModel : BaseViewModel
    {
        private readonly CartViewModel _cartViewModel;

        [ObservableProperty]
        private Pizza _currentPizza;

        public DetailsPageViewModel(CartViewModel cartViewModel)
        {
            _cartViewModel = cartViewModel;
        }

        [RelayCommand]
        private void AddToCart()
        {
            CurrentPizza.Quantity++;
            _cartViewModel.AddPizzaToCartCommand.Execute(CurrentPizza);
        }

        [RelayCommand]  
        private void RmoveFromCart()
        {
            if(CurrentPizza.Quantity > 0)
            {
                CurrentPizza.Quantity--;
                _cartViewModel.RemovePizzaFromCartCommand.Execute(CurrentPizza.Name);
            }
        }


        [RelayCommand]
        private async Task GoToViewCartAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                SetTrueBoolValues();
                await Shell.Current.GoToAsync($"{nameof(CartPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while navigating to cart page: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while navigating to cart page", "Ok");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        protected override void SetFalseBoolValues()
        {
            IsBusy = false;
        }

        protected override void SetTrueBoolValues()
        {
            IsBusy = true;
        }
    }
}
