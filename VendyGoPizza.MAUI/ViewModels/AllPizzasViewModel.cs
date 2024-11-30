
namespace VendyGoPizza.MAUI.ViewModels
{
    [QueryProperty(nameof(IsFromSearch), nameof(IsFromSearch))]
    public partial class AllPizzasViewModel : BaseViewModel
    {
        // Service for manupulating pizza's data
        private readonly PizzaService _pizzaService;

        [ObservableProperty]
        private string _isFromSearch;

        [ObservableProperty]
        private bool _isSearching;

        public ObservableCollection<Pizza> AllPizzas { get; } = new ObservableCollection<Pizza>();

        public AllPizzasViewModel(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
            LoadAllPizzas();
        }

        /// <summary>
        /// Method to load allpizzas collection
        /// </summary>
        /// <param name="pizzaService"></param>
        /// <returns></returns>
        private void LoadAllPizzas()
        {
            try
            {
                var pizzas = _pizzaService
                            .GetAllPizzas()
                            .OrderBy(p => p.Name);

                AllPizzas.Clear(); // Clear any existing items

                foreach (var pizza in pizzas)
                {
                    AllPizzas.Add(pizza); // Add new items
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while loading pizzas: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task SearchPizzaAsync(string searchTerm)
        {
            if(IsBusy)
            {
                return;
            }

            try
            {
                SetTrueBoolValues();

                if (AllPizzas.Count != 0)
                {
                    AllPizzas.Clear();
                }

                // Simulation of delay for indicator activity in UI
                await Task.Delay(500);

                // Get pizzas by search term and add them to the AllPizzas collection
                foreach (var pizza in _pizzaService.GetPizzasBySearchTerm(searchTerm))
                {
                    AllPizzas.Add(pizza);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while searching for pizza: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while searching for pizza", "OK");
            }
            finally
            {
                SetFalseBoolValues();
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
                        [nameof(DetailsPageViewModel.CurrentPizza)] = currentPizza
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

        protected override void SetTrueBoolValues()
        {
            IsBusy = true;
            IsSearching = true;
        }

        protected override void SetFalseBoolValues()
        {
            IsBusy = false;
            IsSearching = false;
        }
    }
}
