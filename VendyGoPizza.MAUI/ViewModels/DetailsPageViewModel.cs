
using System;

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

            // Subscribe to the CartCleared event
            cartViewModel.CartCleared += CartViewModel_OnCartCleared;
            // Subscribe to the CartPizzaRemoved event
            cartViewModel.CartPizzaRemoved += CartViewModel_OnCartPizzaRemoved;
            // Subscribe to the CartPizzaUpdated event
            cartViewModel.CartPizzaUpdated += CartViewModel_OnCartPizzaUpdated;
        }

        private void CartViewModel_OnCartPizzaUpdated(object? sender, Pizza e)
        {
            CartViewModel_OnCartItemChanged(e, e.Quantity);
        }

        private void CartViewModel_OnCartItemChanged(Pizza pizza, int quantity)
        {
            if(CurrentPizza.Name == pizza.Name)
            {
                CurrentPizza.Quantity = quantity;
            }
        }

        private void CartViewModel_OnCartPizzaRemoved(object? sender, Pizza e)
        {
            CartViewModel_OnCartItemChanged(e, 0);
        }

        private void CartViewModel_OnCartCleared(object? sender, EventArgs e)
        {
            CartViewModel_OnCartItemChanged(_currentPizza, 0);
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
        private async Task GoToViewCart()
        {
            if(IsBusy)
            {
                return;
            }

            //if (CurrentPizza.Quantity == 0)
            //{
            //    //await Shell.Current.DisplayAlert("Alert", "Please add pizza to cart", "OK");
            //    await Toast.Make("Please add pizza to cart", ToastDuration.Short)
            //        .Show();
            //    return;
            //}

            try
            {
                SetTrueBoolValues();
                await Shell.Current.GoToAsync(nameof(CartPage), true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while navigating to cart page: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while navigating to cart page", "OK");
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

        private void Dispose()
        {
            _cartViewModel.CartCleared -= CartViewModel_OnCartCleared;
            _cartViewModel.CartPizzaRemoved -= CartViewModel_OnCartPizzaRemoved;
            _cartViewModel.CartPizzaUpdated -= CartViewModel_OnCartPizzaUpdated;
        }
    }
}
