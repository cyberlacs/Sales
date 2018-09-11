namespace Sales.ViewModels
{
    using Common.Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        private ApiServices apiServices;
        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public ProductsViewModel()
        {
            this.apiServices = new ApiServices();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiServices.GetList<Product>("http://www.sistemavoltesempre.com.br/salesapi", "/api", "/product");
            if (response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);
        }
    }
}
