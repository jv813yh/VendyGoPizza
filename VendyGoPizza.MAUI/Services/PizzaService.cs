namespace VendyGoPizza.MAUI.Services
{
    public class PizzaService
    {
        // Static simple list of pizzas for the demo
        private readonly static IEnumerable<Pizza> _pizzas = new List<Pizza>
        {
            new Pizza
            {
                Name = "Pepperoni",
                Image = "pizza1.png",
                Price = 10.99,
                Description = "Pepperoni, tomato sauce, mozzarella cheese, and oregano."
            },
            new Pizza
            {
                Name = "Margherita",
                Image = "pizza2.png",
                Price = 9.99,
                Description = "Tomato sauce, mozzarella cheese, fresh basil, and olive oil."
            },
            new Pizza
            {
                Name = "Hawai",
                Image = "pizza3.png",
                Price = 12.99,
                Description = "Tomato sauce, mozzarella cheese, ham, and pineapple."
            },
            new Pizza
            {
                Name = "Meat Lovers",
                Image = "pizza4.png",
                Price = 14.99,
                Description = "Pepperoni, sausage, bacon, and ham."
            },
            new Pizza
            {
                Name = "Veggie",
                Image = "pizza5.png",
                Price = 11.99,
                Description = "Tomato sauce, mozzarella cheese, bell peppers, onions, and mushrooms."
            },
            new Pizza
            {
                Name = "Greek",
                Image = "pizza6.png",
                Price = 16.99,
                Description = "A light and flavorful pizza featuring feta, tomatoes, and spinach. " +
                "I like to use a homemade whole wheat crust, rolled thin."
            },
            new Pizza
            {
                Name = "Sicilian",
                Image = "pizza7.png",
                Price = 16.99,
                Description = "Onions, anchovies, tomatoes, herbs and strong cheese such as caciocavallo and toma."
            },
            new Pizza
            {
                Name = "California",
                Image = "pizza8.png",
                Price = 16.99,
                Description = "Tomato sauce, mozzarella cheese, bell peppers, onions, and mushrooms."
            }
        };

        /// <summary>
        /// Get all pizzas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Pizza> GetAllPizzas()
            => _pizzas;

        /// <summary>
        /// Get popular pizzas
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<Pizza> GetPopularPizzas(int count = 4)
            => _pizzas.OrderBy(p => Guid.NewGuid())
                      .Take(count);

        /// <summary>
        /// Get pizzas by search term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public IEnumerable<Pizza> GetPizzasBySearchTerm(string searchTerm)
            => string.IsNullOrEmpty(searchTerm) ? 
                _pizzas : 
                _pizzas.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
    }
}
