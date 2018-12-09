using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollector {

    public partial class SensorPack {

        public void Start() {
            if(StartPlatform()) {
                IsRecording = true;
            }
        }

        public void Stop() {
            if (StopPlatform()) {
                IsRecording = false;
            }
        }

        public bool IsRecording { get; private set; }

    }

}
