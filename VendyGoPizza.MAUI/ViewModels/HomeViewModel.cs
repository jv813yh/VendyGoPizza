namespace VendyGoPizza.MAUI.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly PizzaService _pizzaService;

        // Collection for popular pizzas on the home page
        public ObservableCollection<Pizza> Pizzas { get; set; }

        public HomeViewModel(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
            Pizzas = new ObservableCollection<Pizza>(_pizzaService.GetPopularPizzas());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isFromSearch"></param>
        /// <returns></returns>
        /// 
        [RelayCommand]
        public async Task GoToAllPizzasPage(string isFromSearch)
        {
            if(IsBusy)
            {
                return;
            }

            try
            {
                SetTrueBoolValues();
                // Navigate to allPizzasPage with the isFromSearch parameter
                await Shell.Current.GoToAsync($"{nameof(AllPizzasPage)}", true,
                    new Dictionary<string, object>
                    {
                        [nameof(AllPizzasViewModel.IsFromSearch)] = isFromSearch
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while navigating to all pizzas page: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while navigating to all pizzas page", "Ok");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        [RelayCommand]
        private async Task BestOfferAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                // Get random pizza from the list of popular pizzas
                Pizza? randomPizza = Pizzas.OrderBy(p => Guid.NewGuid())
                    .FirstOrDefault(); 

                if(randomPizza != null)
                {
                    await GoToDetialsPageAsync(randomPizza);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while executing best offer command {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while executing best offer command ", "Ok");
            }
        }

            /// <summary>
            /// Navigate to details page with validation for busy state
            /// </summary>
            /// <returns></returns>
        [RelayCommand]
        private async Task GoToDetialsPageAsync(Pizza currentPizza)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                SetTrueBoolValues();
                // Navigate to details page parameter
                await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
                    new Dictionary<string, object>
                    {
                        [nameof(DetailsPageViewModel.CurrentPizza)] = currentPizza,
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while navigating to details page: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while navigating to details page", "Ok");
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
