
using System.ComponentModel;
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
                        return "Vehicle park created"; break;
                    }
                case "Park":
                    {
                        switch (command.Parameters["type"])
                        {
                            case "car":
                                {
                                    return this.ExecuteParkCar(command); break;
                                }
                            case "motorbike":
                                {
                                    return ExecuteParkMotorbike(command); break;
                                   
                                }
                            case "truck":
                                {
                                    return ExecuteParkTruck(command); break;
                                }
                            default:
                                throw new IndexOutOfRangeException("Invalid command inside.");
                        }

                        break;
                    }
                case "Exit":
                    {
                        return VehiclePark.ExitVehicle(
                            command.Parameters["licensePlate"],
                           DateTime.Parse(command.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind),
                            decimal.Parse(command.Parameters["paid"])); break;
                    }
                case "Status":
                    {
                        return VehiclePark.GetStatus(); break;
                    }
                case "FindVehicle":
                    {
                        return VehiclePark.FindVehicle(command.Parameters["licensePlate"]); break;
                    }
                case "VehiclesByOwner":
                    {
                        return VehiclePark.FindVehiclesByOwner(command.Parameters["owner"]); break;
                    }
                default:
                    throw new IndexOutOfRangeException("Invalid command.");

            }
        }

        private string ExecuteParkTruck(ICommand command)
        {
            return VehiclePark.InsertTruck(
                new Truck(command.Parameters["licensePlate"],
                    command.Parameters["owner"], int.Parse(command.Parameters["hours"])),
                int.Parse(command.Parameters["sector"]), int.Parse(command.Parameters["place"]),
                DateTime.Parse(command.Parameters["time"], null,
                    System.Globalization.DateTimeStyles.RoundtripKind));
        }

        private string ExecuteParkMotorbike(ICommand command)
        {
            return VehiclePark.InsertMotorbike(
                new Motorbike(command.Parameters["licensePlate"],
                    command.Parameters["owner"],
                    int.Parse(command.Parameters["hours"])),
                int.Parse(command.Parameters["sector"]),
                int.Parse(command.Parameters["place"]),
                DateTime.Parse(command.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind));
        }

        private string ExecuteParkCar(ICommand command)
        {
            return VehiclePark.InsertCar(
                new Car(command.Parameters["licensePlate"],
                    command.Parameters["owner"],
                    int.Parse(command.Parameters["hours"])),
                int.Parse(command.Parameters["sector"]),
                int.Parse(command.Parameters["place"]),
                DateTime.Parse(command.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind));
        }
    }
}
