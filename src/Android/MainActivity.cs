using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android;
using Plugin.CurrentActivity;

namespace DataCollector.Android {

    [Activity(
        Label = "Data Collector",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
    )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        protected override void OnCreate(Bundle savedInstanceState) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults) {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            var index = Array.IndexOf<string>(permissions, Manifest.Permission.AccessFineLocation);
            if(index >= 0) {
                if(grantResults[index] == Permission.Granted) {
                    System.Diagnostics.Debug.WriteLine("{0} permission granted", Manifest.Permission.AccessFineLocation);
                    Toast.MakeText(this, "Access granted, try again", ToastLength.Short).Show();
                }
                else {
                    System.Diagnostics.Debug.WriteLine("{0} permission not granted", Manifest.Permission.AccessFineLocation);
                    Toast.MakeText(this, "Fine location access not granted!", ToastLength.Long).Show();
                }
            }
        }

    }

}
