
using VehicleParkSystem.Interfaces;
using VehicleParkSystem;

namespace VehicleParkSystem
{
    using System;
    class Dispatcher
    {
        VehiclePark VehiclePark { get; set; }
        public string Execution(ICommand command)
        {
            if (command.Name != "SetupPark" && VehiclePark == null)
            {
                return "The vehicle park has not been set up";
            }
            switch (command.Name)
            {
                case "SetupPark":
                    {
                        //This doesnot work!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        // I donot know why!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        //VehiclePark=new VehiclePark(c.Parameters["sectors"]+1,c.Parameters["placesPerSector"]);
                        return "Vehicle park created";
                    }
                case "Рark":
                    {
                        switch (command.Parameters["type"])
                        {
                            case "car":
                                {
                                    return
                                        VehiclePark.InsertCar(
                                            new Car(command.Parameters["licensePlate"],
                                                command.Parameters["owner"], int.Parse(command.Parameters["hours"])),
                                            int.Parse(command.Parameters["sector"]), int.Parse(command.Parameters["place"]),
                                            DateTime.Parse(command.Parameters["time"], null,
                                                System.Globalization.DateTimeStyles.RoundtripKind)); //why round trip kind??
                                }
                            case "motorbike":
                                {
                                    return
                                        VehiclePark.InsertMotorbike(
                                            new Moto(command.Parameters["licensePlate"],
                                                command.Parameters["owner"], int.Parse(command.Parameters["hours"])),
                                            int.Parse(command.Parameters["sector"]), int.Parse(command.Parameters["place"]),
                                            DateTime.Parse(command.Parameters["time"], null,
                                                System.Globalization.DateTimeStyles.RoundtripKind)); //stack overflow says this
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
                        }
                    }
                    break;





























                case "Exit": return VehiclePark.ExitVehicle(command.Parameters["licensePlate"], DateTime.Parse(command.Parameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind), decimal.Parse(command.Parameters["money"]));
                case "Status": return VehiclePark.GetStatus();
                case "FindVehicle": return VehiclePark.FindVehicle(command.Parameters["licensePlate"]);
                case "VehiclesByOwner": return VehiclePark.FindVehiclesByOwner(command.Parameters["owner"]);
                default: throw new IndexOutOfRangeException("Invalid command.");




            }


            return "";


        }


    }
}
