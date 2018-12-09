using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DataCollector.ViewModels {

    public class MainViewModel : BaseViewModel {

        public MainViewModel() {
            StartCommand = new Command(o => App.GetSensors().Start(o?.ToString()));
            StopCommand = new Command(() => App.GetSensors().Stop());
        }

        public override void Register() {
            var s = App.GetSensors();
            s.RecordingStatusUpdated += RecordingStatusUpdated;
            s.SensorsUpdated += SensorsUpdated;
        }

        public override void Unregister() {
            var s = App.GetSensors();
            s.RecordingStatusUpdated -= RecordingStatusUpdated;
            s.SensorsUpdated -= SensorsUpdated;
        }

        public bool IsRecording {
            get => App.GetSensors().IsRecording;
        }

        private long _count = 0;

        public long Count {
            get => _count;
            set {
                SetProperty(ref _count, value);
            }
        }

        private TimeSpan _elapsedTime = TimeSpan.Zero;

        public TimeSpan ElapsedTime {
            get => _elapsedTime;
            set {
                SetProperty(ref _elapsedTime, value);
            }
        }

        private void RecordingStatusUpdated(object sender, EventArgs e) {
            OnPropertyChanged(nameof(IsRecording));
        }

        private void SensorsUpdated(object sender, SensorsUpdatedEventArgs e) {
            Count = e.Count;
            ElapsedTime = e.ElapsedTime;
        }

        public Command StartCommand { get; private set; }

        public Command StopCommand { get; private set; }

    }

}
