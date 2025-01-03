﻿namespace VendyGoPizza.MAUI.Services
{
    public class PizzaService
    {
        // Static simple list of pizzas for the demo
        private readonly static List<Pizza> _pizzas = new List<Pizza>
        {
            new Pizza
            {
                Name = "Pepperoni",
                Image = "pizza1.png",
                Price = 10.99,
                Ingredients = "pepperoni, tomato sauce, mozzarella cheese, and oregano",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of " +
                "type and scrambled it to make a type specimen book."
            },
            new Pizza
            {
                Name = "Margherita",
                Image = "pizza2.png",
                Price = 9.99,
                Ingredients = "tomato sauce, mozzarella cheese, fresh basil, and olive oil",
                Description = "It has survived not only five centuries, but also the leap into " +
                "electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets " +
                "containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            },
            new Pizza
            {
                Name = "Hawai",
                Image = "pizza3.png",
                Price = 12.99,
                Ingredients = "tomato sauce, mozzarella cheese, ham, and pineapple",
                Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. " +
                "The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', " +
                "making it look like readable English."
            },
            new Pizza
            {
                Name = "Meat Lovers",
                Image = "pizza4.png",
                Price = 14.99,
                Ingredients = "pepperoni, sausage, bacon, and ham",
                Description = "Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' " +
                "will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected " +
                "humour and the like)."
            },
            new Pizza
            {
                Name = "Veggie",
                Image = "pizza5.png",
                Price = 11.99,
                Ingredients = "tomato sauce, mozzarella cheese, bell peppers, onions",
                Description = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, " +
                "making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, " +
                "consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source."
            },
            new Pizza
            {
                Name = "Greek",
                Image = "pizza6.png",
                Price = 16.99,
                Ingredients = "light and flavorful pizza featuring feta, tomatoes, and spinach",
                Description = "Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of \"de Finibus Bonorum et Malorum\" (The Extremes of Good and Evil) by Cicero, written " +
                "in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, \"Lorem ipsum dolor sit amet." +
                ".\", comes from a line in section 1.10.32.."
            },
            new Pizza
            {
                Name = "Sicilian",
                Image = "pizza7.png",
                Price = 16.99,
                Ingredients = "onions, anchovies, tomatoes, herbs and strong cheese such as caciocavallo",
                Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, " +
                "or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything " +
                "embarrassing hidden in the middle of text.."
            },
            new Pizza
            {
                Name = "California",
                Image = "pizza8.png",
                Price = 16.99,
                Ingredients = "tomato sauce, mozzarella cheese, bell peppers and mushrooms",
                Description = "All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the " +
                "Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks " +
                "reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.."
            }
        };

        /// <summary>
        /// Get all pizzas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Pizza> GetAllPizzas()
            => _pizzas;

        //public void SetQuantityForAllPizzas(int quantity)
        //{
        //    foreach (var pizza in _pizzas)
        //    {
        //        pizza.Quantity = quantity;
        //    }
        //}   

        //public void FindAndSetQuantity(Pizza pizzaWanted, int quantity)
        //{
        //    var pizzaFromList = _pizzas
        //                        .FirstOrDefault(p => p.Name == pizzaWanted.Name);

        //    if (pizzaFromList != null &&
        //        quantity >= 0)
        //    {
        //        pizzaFromList.Quantity = quantity;
        //    }
        //}

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
