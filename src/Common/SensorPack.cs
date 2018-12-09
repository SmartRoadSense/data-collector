using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DataCollector {

    public partial class SensorPack {

        const int TimerInterval = 1000; // 1s

        private readonly Timer _timer;

        public SensorPack() {
            _timer = new Timer(TimerTick, null, Timeout.Infinite, Timeout.Infinite);
        }

        private void TimerTick(object state) {
            if(!IsRecording) {
                return;
            }

            Debug.WriteLine("Tick");
            SensorsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void Start() {
            if(StartPlatform()) {
                IsRecording = true;
                RecordingStatusUpdated?.Invoke(this, EventArgs.Empty);

                _timer.Change(TimerInterval, TimerInterval);
            }
        }

        public void Stop() {
            if (StopPlatform()) {
                IsRecording = false;
                RecordingStatusUpdated?.Invoke(this, EventArgs.Empty);

                _timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        public bool IsRecording { get; private set; }

        public event EventHandler SensorsUpdated;

        public event EventHandler RecordingStatusUpdated;

    }

}
