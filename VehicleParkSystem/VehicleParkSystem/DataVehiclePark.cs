using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleParkSystem.Interfaces;
using Wintellect.PowerCollections;

namespace VehicleParkSystem
{
    class Data
    {
        public Data(int numberOfSectors)
        {
           this.VehicleInPark = new Dictionary<IVehicle, string>(); 
            VehicleByLocation = new Dictionary<string, IVehicle>();
            VehicleByLicensePlate = new Dictionary<string, IVehicle>();
            VehicleByStartTime = new Dictionary<IVehicle, DateTime>(); 
            VehcileByOwner = new MultiDictionary<string, IVehicle>(false);
            sectors = new int[numberOfSectors];
        }

        public Dictionary<IVehicle, string> VehicleInPark { get; set; }
        public Dictionary<string, IVehicle> VehicleByLocation { get; set; }
        public Dictionary<string, IVehicle> VehicleByLicensePlate { get; set; }
        public Dictionary<IVehicle, DateTime> VehicleByStartTime { get; set; }
        public MultiDictionary<string, IVehicle> VehcileByOwner { get; set; }
        public int[] sectors { get; set; }



    }

}
