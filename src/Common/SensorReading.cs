using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollector {

    public class SensorReading {

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string LocationProvider { get; set; }
        public float Speed { get; set; }
        public float Accuracy { get; set; }

        public double AccelerationX { get; set; }
        public double AccelerationY { get; set; }
        public double AccelerationZ { get; set; }
        public string AccelerationAccuracy { get; set; }

        public double GyroX { get; set; }
        public double GyroY { get; set; }
        public double GyroZ { get; set; }

    }

}
