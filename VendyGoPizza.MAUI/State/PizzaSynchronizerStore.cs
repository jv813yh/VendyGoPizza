using System.ComponentModel;

namespace VendyGoPizza.MAUI.State
{
    /// <summary>
    /// Manage all originals and clones and ensure their synchronization
    /// </summary>
    public class PizzaSynchronizerStore
    {
        /// <summary>
        /// Key - original pizza, Value - clone pizza
        /// </summary>
        private readonly Dictionary<Pizza, Pizza> _synchronizationObjects;


        public PizzaSynchronizerStore()
        {
            _synchronizationObjects = new Dictionary<Pizza, Pizza>();
        }

        /// <summary>
        /// Add pair to the store
        /// </summary>
        /// <param name="original"></param>
        /// <param name="clone"></param>
        public void AddPair(Pizza original, Pizza clone)
        {
            // Check if the original pizza is already in the store
            bool isNotInDictionary = _synchronizationObjects.TryGetValue(original, out var existingClone);

            if (!isNotInDictionary)
            {
                _synchronizationObjects.Add(original, clone);

                //original.PropertyChanged += (s, e) =>
                //{
                //    if (e.PropertyName == nameof(Pizza.Quantity))
                //    {
                //        clone.Quantity = original.Quantity;
                //    }
                //};
                //clone.PropertyChanged += (s, e) =>
                //{
                //    if (e.PropertyName == nameof(Pizza.Quantity))
                //    {
                //        original.Quantity = clone.Quantity;
                //    }
                //};
            }
        }


        /// <summary>
        /// Remove pair from the store
        /// </summary>
        /// <param name="clone"></param>
        public (Pizza?, int) RemovePair(Pizza clone)
        {
            // Check if the clone pizza is in the store
            bool isContains = _synchronizationObjects
                              .ContainsValue(clone);

            if (isContains)
            {
                // Find the original pizza by the clone
                Pizza key = _synchronizationObjects
                            .FirstOrDefault(Pizza => Pizza.Value == clone)
                            .Key;

                // Get the quantity of the clone pizza
                int quantity = key.Quantity;

                // Set the quantity of the original pizza to 0
                key.Quantity = 0;

                // Remove the pair from the store
                _synchronizationObjects.Remove(key);

                return (key, quantity);
            }

            return (null, -1);
        }

        /// <summary>
        /// Clear the store
        /// </summary>
        public void ClearStore()
        {
            // Set the quantity of all original pizzas to 0
            _synchronizationObjects
                .Keys
                .ToList()
                .ForEach(p => p.Quantity = 0);

            // Clear the store
            _synchronizationObjects.Clear();
        }  
    }
}
