using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace VehicleParkSystem
{
    class Data
    {
        public Data(int numberOfSectors)
        {
            carros_inpark = new Dictionary<IVehicle, string>(); park = new Dictionary<string, IVehicle>(); números = new Dictionary<string, IVehicle>(); d = new Dictionary<IVehicle, DateTime>(); ow = new MultiDictionary<string, IVehicle>(false); count = new int[numberOfSectors];
        }
        #region Hard Stuff! My boss wrote that
        public Dictionary<IVehicle, string> carros_inpark { get; set; }public Dictionary<string, IVehicle> park { get; set; }public Dictionary<string, IVehicle> números { get; set; }public Dictionary<IVehicle, DateTime> d { get; set; }public MultiDictionary<string, IVehicle> ow { get; set; }public int[] count { get; set; }
        #endregion
    }

}
