using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace DataCollector {

    public partial class SensorPack {

        public const int TimerInterval = AccelerometerDesiredDelayMs;

        public const int AccelerometerDesiredHz = 100;
        public const int AccelerometerDesiredDelayMs = 1000 / AccelerometerDesiredHz;

        public const int LocationDesiredHz = 1;
        public const int LocationDesiredDelayMs = 1000 / LocationDesiredHz;

        private readonly Timer _timer;

        public SensorPack() {
            _timer = new Timer(TimerTick, null, Timeout.Infinite, Timeout.Infinite);
            Reading = new SensorReading();

            InitPlatform();
        }

        public SensorReading Reading { get; set; }

        private void TimerTick(object state) {
            if(!IsRecording) {
                return;
            }

            Debug.WriteLine("Tick");
            SensorsUpdated?.Invoke(this, EventArgs.Empty);

            _output.WriteLine(string.Format(
                CultureInfo.InvariantCulture,
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},",
                Reading.LocationProvider ?? "None",
                Reading.Latitude,
                Reading.Longitude,
                Reading.Speed,
                Reading.Accuracy,
                Reading.AccelerationX,
                Reading.AccelerationY,
                Reading.AccelerationZ,
                Reading.GyroX,
                Reading.GyroY,
                Reading.GyroZ
            ));
        }

        public void Start() {
            if(StartPlatform()) {
                IsRecording = true;
                RecordingStatusUpdated?.Invoke(this, EventArgs.Empty);

                StartWritingFile(null);

                _timer.Change(TimerInterval, TimerInterval);
            }
        }

        public void Stop() {
            if (StopPlatform()) {
                IsRecording = false;
                RecordingStatusUpdated?.Invoke(this, EventArgs.Empty);

                StopWritingFile();

                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        private StreamWriter _output = null;

        private void StartWritingFile(string addendum) {
            StopWritingFile();

            (var fs, var isNew) = FileNaming.OpenDumpFile(addendum);
            _output = new StreamWriter(fs);

            if(isNew) {
                _output.WriteLine("Provider,Latitude,Longitude,Speed,Accuracy,AccX,AccY,AccZ,GyroX,GyroY,GyroZ,"),
            }
        }

        private void StopWritingFile() {
            if (_output != null) {
                _output.Dispose();
                _output = null;
            }
        }

        public bool IsRecording { get; private set; }

        public event EventHandler SensorsUpdated;

        public event EventHandler RecordingStatusUpdated;

    }

}
