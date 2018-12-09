using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DataCollector.ViewModels {

    public class MainViewModel : BaseViewModel {

        public MainViewModel() {
            StartCommand = new Command(() => App.GetSensors().Start());
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

        private void RecordingStatusUpdated(object sender, EventArgs e) {
            OnPropertyChanged(nameof(IsRecording));
        }

        private void SensorsUpdated(object sender, EventArgs e) {

        }

        public Command StartCommand { get; private set; }

        public Command StopCommand { get; private set; }

    }

}
