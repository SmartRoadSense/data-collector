using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DataCollector {

    public partial class App : Application {

        public SensorPack Sensors { get; private set; }

        public App() {
            InitializeComponent();

            Sensors = new SensorPack();

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

        public static SensorPack GetSensors() {
            return ((App)App.Current).Sensors;
        }

    }

}
