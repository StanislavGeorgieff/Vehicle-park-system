

using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text.RegularExpressions;
using VehicleParkSystem;
using VehicleParkSystem.Interfaces;
using VehicleParkSystem.Models;
using Wintellect.PowerCollections;

namespace VehicleParkSystem
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;



    internal class VehiclePark : IVehiclePark
    {
        private Layout layout;
        private Data data;

        public VehiclePark(int numberOfSectors, int placesPerSector)
        {
            layout = new Layout(numberOfSectors, placesPerSector);
            data = new Data(numberOfSectors);
        }

        //Car car, int sector, int placeNumber, DateTime startTime
        public string InsertCar(Car car, int sector, int placeNumber, DateTime startTime)
        {

            return InsertVehicle(car, sector, placeNumber, startTime);
        }

        public string InsertMotorbike(Motorbike motorbike, int sector, int placeNumber, DateTime startTime)
        {
            return InsertVehicle(motorbike, sector, placeNumber, startTime);
        }

        public string InsertTruck(Truck truck, int sector, int placeNumber, DateTime startTime)
        {
            return InsertVehicle(truck, sector, placeNumber, startTime);
        }

        public string ExitVehicle(string licensePlate, DateTime endTime, decimal amountPaid)
        {
            var vehicle = (data.VehicleByLicensePlate.ContainsKey(licensePlate)) ? data.VehicleByLicensePlate[licensePlate] : null;
            if (vehicle == null)
            {
                return string.Format("There is no vehicle with license plate {0} in the park", licensePlate);
            }



            var start = data.VehicleByStartTime[vehicle];
            int end = (int)Math.Round((endTime - start).TotalHours);
            var ticket = new StringBuilder();
            decimal regularRate = vehicle.ReservedHours * vehicle.RegularRate;
            decimal overtimeRate = end > vehicle.ReservedHours ? (end - vehicle.ReservedHours) * vehicle.OvertimeRate : 0;
            decimal total = regularRate + overtimeRate;
            ticket.AppendLine(new string('*', 20))
                .AppendFormat("{0} [{1}], owned by {2}", vehicle.GetType().Name, vehicle.LicensePlate, vehicle.Owner).AppendLine()
                .AppendFormat("at place {0}", data.LocationByVehicle[vehicle]).AppendLine()
                .AppendFormat("Rate: ${0:F2}", regularRate).AppendLine()
                .AppendFormat("Overtime rate: ${0:F2}", overtimeRate).AppendLine()
                .AppendLine(new string('-', 20))
                .AppendFormat("Total: ${0:F2}", regularRate + overtimeRate).AppendLine()
                .AppendFormat("Paid: ${0:F2}", amountPaid)
                .AppendLine()
                .AppendFormat("Change: ${0:F2}", amountPaid - total).AppendLine()
                .Append(new string('*', 20));

            data.RemoveVehicleFromData(vehicle);

          //bottleneck


            return ticket.ToString();


        }

        public string GetStatus()
        {
            //Sector 1: 0 / 5 (0% full)

            var sectors = data.sectors.Select((occupancy, sector) => string.Format
                ("Sector {0}: {1} / {2} ({3}% full)", sector + 1, occupancy, layout.PlacesPerSector,
                    Math.Round((double)occupancy / layout.PlacesPerSector * 100)
                ));

            return string.Join(Environment.NewLine, sectors);
        }

        public string FindVehicle(string licensePlate)
        {
            var vehicle = data.VehicleByLicensePlate.ContainsKey(licensePlate) ? data.VehicleByLicensePlate[licensePlate] : null;
            if (vehicle == null)
            {
                return string.Format("There is no vehicle with license plate {0} in the park", licensePlate);

            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} [{1}], owned by {2}", vehicle.GetType().Name, vehicle.LicensePlate, vehicle.Owner).AppendLine();
                sb.AppendFormat("Parked at {0}", data.LocationByVehicle[vehicle]).AppendLine();
                return sb.ToString();

            }
        }

        public string FindVehiclesByOwner(string owner)
        {
            if (!data.VehcileByOwner.ContainsKey(owner) || data.VehcileByOwner[owner] == null)
            {
                return string.Format("No vehicles by {0}", owner);
            }

            else
            {
                //bottleneck diferent data for serching.
                StringBuilder sb = new StringBuilder();
                var foundVehicles = data.VehcileByOwner[owner];
                foreach (var vehicle in foundVehicles)
                {

                    sb.AppendFormat("{0} [{1}], owned by {2}", vehicle.GetType().Name, vehicle.LicensePlate, vehicle.Owner).AppendLine();
                    sb.AppendFormat("Parked at {0}", data.LocationByVehicle[vehicle]).AppendLine();
                }
                return sb.ToString();

            }
        }

        public string InsertVehicle(IVehicle vehicle, int sector, int placeNumber, DateTime startTime)
        {
            if (sector > layout.NumberOfSectors)
            {
                return string.Format("There is no sector {0} in the park", sector);
            }
            if (placeNumber > layout.PlacesPerSector)
            {
                return string.Format("There is no place {0} in sector {1}", placeNumber, sector);
            }
            if (data.VehicleByLocation.ContainsKey(string.Format("({0},{1})", sector, placeNumber)))
            {
                return string.Format("The place ({0},{1}) is occupied", sector, placeNumber);
            }
            if (data.VehicleByLicensePlate.ContainsKey(vehicle.LicensePlate))
            {
                return string.Format("There is already a vehicle with license plate {0} in the park",
                    vehicle.LicensePlate);
            }
            data.AddVehicle(vehicle, sector, placeNumber, startTime);
            return string.Format("{0} parked successfully at place ({1},{2})", vehicle.GetType().Name, sector, placeNumber);
        }

    
    }
}