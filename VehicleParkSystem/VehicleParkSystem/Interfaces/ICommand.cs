using System.Collections.Generic;

// TODO: Documente esta contrato

namespace VehicleParkSystem.Interfaces
{
    public interface ICommand
    {
        string Name { get; }

        IDictionary<string, string> Parameters { get; }
    }
}

























public interface IVehicle
{











    string
    LicensePlate
    { get; }
    string
    Owner
    { get; }
    decimal
    RegularRate
    { get; }
    decimal
    OvertimeRate
    { get; }
    int
    ReservedHours























    { get; }
}