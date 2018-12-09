using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;

namespace DataCollector {

    public partial class SensorPack {

        public bool StartPlatform() {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M) {
                if (ContextCompat.CheckSelfPermission(CrossCurrentActivity.Current.AppContext, Manifest.Permission.AccessFineLocation) != global::Android.Content.PM.Permission.Granted) {
                    ActivityCompat.RequestPermissions(CrossCurrentActivity.Current.Activity, new string[] {
                        Manifest.Permission.AccessFineLocation
                    }, 123);

                    return false;
                }
            }

            return true;
        }

        public bool StopPlatform() {
            return true;
        }

    }

}
