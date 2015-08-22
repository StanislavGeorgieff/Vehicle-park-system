using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleParkSystem.Interfaces;
using VehicleParkSystem.Models;
using Wintellect.PowerCollections;

namespace VehicleParkSystem
{
    class Data
    {
        public Data(int numberOfSectors)
        {
            this.LocationByVehicle = new Dictionary<IVehicle, string>();
            this.VehicleByLocation = new Dictionary<string, IVehicle>();
            this.VehicleByLicensePlate = new Dictionary<string, IVehicle>();
            this.VehicleByStartTime = new Dictionary<IVehicle, DateTime>();
            this.VehcileByOwner = new MultiDictionary<string, IVehicle>(false);
            this.sectors = new int[numberOfSectors];
        }

        public Dictionary<IVehicle, string> LocationByVehicle { get; set; }
        public Dictionary<string, IVehicle> VehicleByLocation { get; set; }
        public Dictionary<string, IVehicle> VehicleByLicensePlate { get; set; }
        public Dictionary<IVehicle, DateTime> VehicleByStartTime { get; set; }
        public MultiDictionary<string, IVehicle> VehcileByOwner { get; set; }
        public int[] sectors { get; set; }

        public void AddVehicle(IVehicle vehicle, int sector, int placeNumber, DateTime startTime)
        {
            LocationByVehicle[vehicle] = string.Format("({0},{1})", sector, placeNumber);
            VehicleByLocation[string.Format("({0},{1})", sector, placeNumber)] = vehicle;
            VehicleByLicensePlate[vehicle.LicensePlate] = vehicle;
            VehicleByStartTime[vehicle] = startTime;
            VehcileByOwner[vehicle.Owner].Add(vehicle);
            sectors[sector - 1]++;
        }

        public void RemoveVehicleFromData(IVehicle vehicle)
        {
            VehicleByLocation.Remove(LocationByVehicle[vehicle]);
            VehicleByLicensePlate.Remove(vehicle.LicensePlate);
            VehicleByStartTime.Remove(vehicle);
            VehcileByOwner[vehicle.Owner].Remove(vehicle);
            int sector = int.Parse(this.LocationByVehicle[vehicle].Split(new[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries)[0]); ;
            LocationByVehicle.Remove(vehicle);
            sectors[sector - 1]--;
        }


    }

}
