namespace VendyGoPizza.MAUI.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        public bool IsNotBusy 
            => !IsBusy;

        /// <summary>
        /// Abstract method to set the values of the boolean properties to true
        /// according the implementation
        /// </summary>
        protected abstract void SetTrueBoolValues();

        /// <summary>
        /// Abstract method to set the values of the boolean properties to false
        /// according the implementation
        /// </summary>
        protected abstract void SetFalseBoolValues();
    }
}
