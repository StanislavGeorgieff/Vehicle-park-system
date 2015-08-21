using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleParkSystem.Interfaces
{

    public interface IVehicle
    {
        string LicensePlate { get; }
        string Owner { get; }
        decimal RegularRate { get; }
        decimal OvertimeRate { get; }
        int ReservedHours { get; }
    }
}
