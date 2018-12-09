using System;
using System.Collections.Generic;
using System.Text;

namespace DataCollector {

    public class SensorsUpdatedEventArgs : EventArgs {

        public long Count { get; set; }

        public TimeSpan ElapsedTime { get; set; }

    }

}
