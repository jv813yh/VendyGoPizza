namespace VendyGoPizza.MAUI.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        // Total amount of pizza'đ in the cart
        [ObservableProperty]
        private decimal _totalAmount;

        [ObservableProperty]
        private bool _isVisibleButtonHome;

        // Event to notify when a pizza is removed from the cart
        public event EventHandler<Pizza> CartPizzaRemoved;
        // Event to notify when a pizza is updated in the cart
        public event EventHandler<Pizza> CartPizzaUpdated;
        // Event to notify when the cart is cleared
        public event EventHandler CartCleared;

        // List of pizzas in the cart
        public ObservableCollection<Pizza> CartPizzas { get; private set; }


        public CartViewModel()
        {
            CartPizzas = new ObservableCollection<Pizza>();
        }

        /// <summary>
        /// Command to add pizza to cart  
        /// </summary>
        /// <param name="pizza"></param>
        [RelayCommand]
        private void AddPizzaToCart(Pizza pizza)
        {
            // Check if the pizza is already in the cart
            var cartPizza = CartPizzas.FirstOrDefault(p => p.Name == pizza.Name);

            if (cartPizza == null)
            {
                CartPizzas.Add(pizza.Clone());
            }
            else
            {
                cartPizza.Quantity = pizza.Quantity;
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
            var cartPizza = CartPizzas.FirstOrDefault(p => p.Name == name);

            try
            {
                SetTrueBoolValues();

                if (cartPizza != null)
                {
                    CartPizzas.Remove(cartPizza);
                    RecalculateTotalAmount();

                    // Notify that the pizza is removed from the cart
                    CartPizzaRemoved?.Invoke(this, cartPizza);

                    // Show snackbar to undo the action
                    var snackBarOptions = new SnackbarOptions()
                    {
                        CornerRadius = 20,
                        TextColor = Colors.Black,
                        BackgroundColor = Colors.PaleGoldenrod,
                    };

                    var snackBar = Snackbar.Make($"{name} removed from cart",
                        () =>
                        {
                            CartPizzas.Add(cartPizza);
                            RecalculateTotalAmount();

                            // Notify that the pizza is added back to the cart
                            CartPizzaUpdated?.Invoke(this, cartPizza);

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
                bool result = await Shell.Current.DisplayAlert("Alert", "Are you sure you want to clear the cart?", "Yes", "No");

                if (result  
                    && CartPizzas.Count > 0)
                {
                    SetTrueBoolValues();

                    CartPizzas.Clear();
                    RecalculateTotalAmount();

                    // Notify that the cart is cleared
                    CartCleared?.Invoke(this, EventArgs.Empty);
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
                    CartCleared?.Invoke(this, EventArgs.Empty);
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
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while navigating to HomePage: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while navigating to Home page", "OK");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        private void RecalculateTotalAmount()
        {
            TotalAmount = CartPizzas.Sum(p => p.Amount);

            IsVisibleButtonHome = CartPizzas.Any();
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
