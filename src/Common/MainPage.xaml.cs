using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataCollector.ViewModels;
using Xamarin.Forms;

namespace DataCollector {

    public partial class MainPage : ContentPage {

        private readonly MainViewModel _viewModel = new MainViewModel();

        public MainPage() {
            InitializeComponent();

            BindingContext = _viewModel;
        }

        public MainViewModel ViewModel {
            get => _viewModel;
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            _viewModel.Register();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();

            _viewModel.Unregister();
        }

    }

}
