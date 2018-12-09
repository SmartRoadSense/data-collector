using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DataCollector {

    public partial class App : Application {

        public App() {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart() {
            OnStartPlatform();
        }

        protected override void OnSleep() {
            OnSleepPlatform();
        }

        protected override void OnResume() {
            OnResumePlatform();
        }

    }

}
