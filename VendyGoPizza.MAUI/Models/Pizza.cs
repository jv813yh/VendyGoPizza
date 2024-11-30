using System.ComponentModel;

namespace VendyGoPizza.MAUI.Models
{
    public partial class Pizza : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Amount))]
        private int _quantity;

        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public decimal Amount 
            => Quantity * (decimal)Price;

        public Pizza? Clone() 
            => MemberwiseClone() as Pizza;


        public override bool Equals(object? obj)
        {
            if (obj is Pizza parameterPizza)
            {
                return string.Equals(this.Name, parameterPizza.Name, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
