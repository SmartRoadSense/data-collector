using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;

namespace DataCollector {

    public partial class SensorPack : Java.Lang.Object, ILocationListener, ISensorEventListener {

        private LocationManager _locationManager;
        private SensorManager _sensorManager;
        private PowerManager.WakeLock _wakeLock;

        private void InitPlatform() {
            _locationManager = (LocationManager)CrossCurrentActivity.Current.AppContext.GetSystemService(Context.LocationService);

            _sensorManager = (SensorManager)CrossCurrentActivity.Current.AppContext.GetSystemService(Context.SensorService);

            var pm = (PowerManager)CrossCurrentActivity.Current.AppContext.GetSystemService(Context.PowerService);
            _wakeLock = pm.NewWakeLock(WakeLockFlags.Full, "SmartRoadSense Data Collector sensing");
        }

        private bool StartPlatform() {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M) {
                if (ContextCompat.CheckSelfPermission(CrossCurrentActivity.Current.AppContext, Manifest.Permission.AccessFineLocation) != global::Android.Content.PM.Permission.Granted) {
                    ActivityCompat.RequestPermissions(CrossCurrentActivity.Current.Activity, new string[] {
                        Manifest.Permission.AccessFineLocation
                    }, 123);

                    return false;
                }
            }

            if (!_wakeLock.IsHeld) {
                _wakeLock.Acquire();
            }

            _locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);

            _sensorManager.RegisterListener(this,
                _sensorManager.GetDefaultSensor(SensorType.Accelerometer),
                (SensorDelay)AccelerometerDesiredDelayMs);
            _sensorManager.RegisterListener(this,
                _sensorManager.GetDefaultSensor(SensorType.Gyroscope),
                (SensorDelay)AccelerometerDesiredDelayMs);

            return true;
        }

        private bool StopPlatform() {
            _locationManager.RemoveUpdates(this);

            _sensorManager.UnregisterListener(this);

            if (_wakeLock.IsHeld) {
                _wakeLock.Release();
            }

            return true;
        }

        #region ILocationListener

        public void OnLocationChanged(Location location) {
            Reading.LocationProvider = location.Provider;
            Reading.Latitude = location.Latitude;
            Reading.Longitude = location.Longitude;
            Reading.Speed = location.Speed;
            Reading.Accuracy = location.Accuracy;
        }

        public void OnProviderDisabled(string provider) {

        }

        public void OnProviderEnabled(string provider) {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) {

        }

        #endregion

        #region ISensorEventListener

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy) {
            if (sensor.Type == SensorType.Accelerometer) {
                Reading.AccelerationAccuracy = accuracy.ToString();
            }
        }

        public void OnSensorChanged(SensorEvent e) {
            if(e.Sensor.Type == SensorType.Accelerometer) {
                Reading.AccelerationX = e.Values[0];
                Reading.AccelerationY = e.Values[1];
                Reading.AccelerationZ = e.Values[2];
            }
            else if(e.Sensor.Type == SensorType.Gyroscope) {
                Reading.GyroX = e.Values[0];
                Reading.GyroY = e.Values[1];
                Reading.GyroZ = e.Values[2];
            }
        }

        #endregion

    }

}
