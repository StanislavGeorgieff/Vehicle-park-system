
using System.Runtime.Remoting.Messaging;
using VehicleParkSystem.Interfaces;
using VehicleParkSystem;
using VehicleParkSystem.Models;

namespace VehicleParkSystem
{
    using System;

    internal class Dispatcher
    {
        private VehiclePark VehiclePark { get; set; }

        public string Execution(ICommand command)
        {
            if (command.Name != "SetupPark" && VehiclePark == null)
            {
                return "The vehicle park has not been set up";
            }
            switch (command.Name)
            {
                //SetupPark {"sectors": 3, "placesPerSector": 5}
                case "SetupPark":
                    {
                        int iniciatedSectors = int.Parse(command.Parameters["sectors"]);
                        int initiatedPlacesPerSector = int.Parse(command.Parameters["placesPerSector"]);
                        if (iniciatedSectors <= 0)
                        {
                            return "The number of sectors must be positive";
                        }

                        if (initiatedPlacesPerSector <= 0)
                        {
                            return "The number of places per sector must be positive";
                        }

                        this.VehiclePark = new VehiclePark(iniciatedSectors, initiatedPlacesPerSector);
                        return "Vehicle park created";
                    }
                case "Park":
                    {
                        switch (command.Parameters["type"])
                        {
                            case "car":
                                {
                                    return
                                        VehiclePark.InsertCar(
                                            new Car(command.Parameters["licensePlate"],
                                                    command.Parameters["owner"],
                                          int.Parse(command.Parameters["hours"])),
                                          int.Parse(command.Parameters["sector"]),
                                          int.Parse(command.Parameters["place"]),
                                     DateTime.Parse(command.Parameters["time"], null,System.Globalization.DateTimeStyles.RoundtripKind)); //why round trip kind??
                                }
                            case "motorbike":
                                {//motorbike, int sector, int placeNumber, DateTime startTime
                                    return
                                        VehiclePark.InsertMotorbike(
                                             new Motorbike(command.Parameters["licensePlate"],
                                                    command.Parameters["owner"],
                                          int.Parse(command.Parameters["hours"])), 
                                          int.Parse(command.Parameters["sector"]),
                                          int.Parse(command.Parameters["place"]),
                                     DateTime.Parse(command.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind));
                                    //stack overflow says this
                                }
                            case "truck":
                                {
                                    return
                                        VehiclePark.InsertTruck(
                                            new Truck(command.Parameters["licensePlate"],
                                                command.Parameters["owner"], int.Parse(command.Parameters["hours"])),
                                            int.Parse(command.Parameters["sector"]), int.Parse(command.Parameters["place"]),
                                            DateTime.Parse(command.Parameters["time"], null,
                                                System.Globalization.DateTimeStyles.RoundtripKind)); //I wanna know
                                }
                            default:
                                throw new IndexOutOfRangeException("Invalid command inside.");
                        }
                    }
                case "Exit":
                    {
                        return VehiclePark.ExitVehicle(
                            command.Parameters["licensePlate"],
                           DateTime.Parse(command.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind),
                            decimal.Parse(command.Parameters["paid"]));
                    }
                case "Status":
                    {
                        return VehiclePark.GetStatus();
                    }
                case "FindVehicle":
                    {
                        return VehiclePark.FindVehicle(command.Parameters["licensePlate"]);
                    }
                case "VehiclesByOwner":
                    {
                        return VehiclePark.FindVehiclesByOwner(command.Parameters["owner"]);
                    }
                default:
                    throw new IndexOutOfRangeException("Invalid command.");

            }
        }
    }
}
