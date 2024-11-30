using System.Windows.Input;
using VendyGoPizza.MAUI.State;

namespace VendyGoPizza.MAUI.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        // Total amount of pizza'đ in the cart
        [ObservableProperty]
        private decimal _totalAmount;

        [ObservableProperty]
        private bool _isVisibleButtonHome;

        // For synchronization of pizzas in the cart,
        // with the original pizzas and their clones
        private PizzaSynchronizerStore _pizzaSynchronizerStore;

        // List of pizzas in the cart
        public ObservableCollection<Pizza> CartPizzas { get; private set; }


        public CartViewModel(PizzaSynchronizerStore pizzaSynchronizerStore)
        {
            CartPizzas = new ObservableCollection<Pizza>();

            _pizzaSynchronizerStore = pizzaSynchronizerStore;
        }

        /// <summary>
        /// Command to add pizza to cart  
        /// </summary>
        /// <param name="pizza"></param>
        [RelayCommand]
        private void AddPizzaToCart(Pizza originalPizza)
        {
            // Check if the pizza is already in the cart
            var cartPizza = CartPizzas
                            .FirstOrDefault(p => p.Name == originalPizza.Name);

            if (cartPizza == null)
            {
                // Clone the pizza
                Pizza clonePizza = originalPizza.Clone();
                // Add clone to the cart
                CartPizzas.Add(clonePizza);
                // Add pair to the store
                _pizzaSynchronizerStore.AddPair(originalPizza, clonePizza);
            }
            else
            {
                cartPizza.Quantity = originalPizza.Quantity;
            }

            RecalculateTotalAmount();
        }

        /// <summary>
        /// Command to remove pizza from cart with snackbar to undo the action
        /// </summary>
        /// <param name="name"></param>
        [RelayCommand]
        private async Task RemovePizzaFromCartAsync(string name)
        {
            if(IsBusy)
            {
                return;
            }

            // Check if the pizza is in the cart
            var cartPizza = CartPizzas
                            .FirstOrDefault(p => p.Name == name);
            try
            {
                SetTrueBoolValues();

                if (cartPizza != null)
                {
                    // Remove the pizza from the cart
                    CartPizzas.Remove(cartPizza);

                    // Remove pair from the store
                    // and get original pizza and quantity
                    (var pizzaKey, var quantity) = _pizzaSynchronizerStore
                                                   .RemovePair(cartPizza);

                    RecalculateTotalAmount();

                    // Show snackbar to undo the action
                    var snackBarOptions = new SnackbarOptions()
                    {
                        CornerRadius = 20,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.PaleGoldenrod,
                    };

                    // Show snackbar to undo the action
                    var snackBar = Snackbar.Make($"{name} removed from cart",
                        () =>
                        {
                            // Set properties back correctly
                            pizzaKey.Quantity = quantity;
                            CartPizzas.Add(cartPizza);
                            _pizzaSynchronizerStore.AddPair(pizzaKey, cartPizza);
                            RecalculateTotalAmount();

                        }, "Undo", TimeSpan.FromSeconds(4), snackBarOptions);

                    await snackBar.Show();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while removing pizza from cart: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while removing pizza from cart", "OK");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        /// <summary>
        /// Async Command to clear the cart
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task ClearCartAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                bool result = await Shell.Current.DisplayAlert("Alert", 
                    "Are you sure you want to clear the cart?", 
                    "Yes", 
                    "No");

                if (result  
                    && CartPizzas.Count > 0)
                {
                    SetTrueBoolValues();

                    // Clear the cart
                    CartPizzas.Clear();
                    // Clear the store
                    _pizzaSynchronizerStore.ClearStore();
                    RecalculateTotalAmount();

                    //await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"An error occurred while clearing the cart: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while clearing the cart", "OK");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        [RelayCommand]
        private async Task PlaceOrderAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                if(CartPizzas.Count > 0)
                {
                    SetTrueBoolValues();
                    CartPizzas.Clear();
                    _pizzaSynchronizerStore.ClearStore();
                    RecalculateTotalAmount();

                    // Go to checkout page
                    await Shell.Current.GoToAsync(nameof(CheckoutPage), true);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while placing order: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while placing order", "OK");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        /// <summary>
        /// Async Command to navigate to the Details page with the last pizza in the cart
        /// </summary>
        public ICommand BackToDetailsPageAsyncCommand => new Command(async () =>
        {
            if(IsBusy)
            {
                return;
            }
            try
            {
                SetTrueBoolValues();
                await Shell.Current.GoToAsync(nameof(DetailsPage), false,
                    new Dictionary<string, object>
                    {
                        [nameof(DetailsPageViewModel.CurrentPizza)] = CartPizzas.Last(),
                    });
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"An error occurred while navigating to Details page: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while navigating to Details page", "OK");
            }
            finally
            {
                SetFalseBoolValues();
            }
        });

        [RelayCommand]
        private async Task GoToHomePageAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                SetTrueBoolValues();
                //await Shell.Current.GoToAsync($"//{nameof(HomePage)}");

                // Pop to the root page,
                // because the HomePage is the first page in the navigation stack 
                // PopToRootAsync() delete all pages from the navigation stack except the root page
                await Shell.Current.Navigation.PopToRootAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while navigating to MainPaige: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while navigating to Main page", "OK");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        /// <summary>
        /// Recalculate the total amount of pizzas in the cart 
        /// and set the visibility of the home button
        /// </summary>
        private void RecalculateTotalAmount()
        {
            TotalAmount = CartPizzas
                         .Sum(p => p.Amount);

            IsVisibleButtonHome = CartPizzas
                                  .Any();
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
