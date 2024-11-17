namespace VendyGoPizza.MAUI.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        // Total amount of pizza'đ in the cart
        [ObservableProperty]
        private decimal _totalAmount;

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
        /// Command to remove pizza from cart
        /// </summary>
        /// <param name="name"></param>
        [RelayCommand]
        private void RemovePizzaFromCart(string name)
        {
            var cartPizza = CartPizzas.FirstOrDefault(p => p.Name == name);

            if (cartPizza != null)
            {
                CartPizzas.Remove(cartPizza);
            }

            RecalculateTotalAmount();
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
                    RecalculateTotalAmount();

                    // Go to checkout page
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        private void RecalculateTotalAmount()
        {
            TotalAmount = CartPizzas.Sum(p => p.Amount);
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
