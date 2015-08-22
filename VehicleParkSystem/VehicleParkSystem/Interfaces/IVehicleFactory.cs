using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleParkSystem.Interfaces
{
    public interface IVehicleFactory
    {
        IVehicle GetVehicle(string licenseplate, string owner, int reservedHours);
    }
}
