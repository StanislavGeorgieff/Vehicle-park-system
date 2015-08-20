

using System;
using System.Collections.Generic;
using VehicleParkSystem;
using VehicleParkSystem.Interfaces;
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

        public string InsertCar(Car carro, int s, int p, DateTime t)
        {
            if (s > layout.sectors)
            {
                return string.Format("There is no sector {0} in the park", s);
            }
            if (p > layout.places_sec)
            {
                return string.Format("There is no place {0} in sector {1}", p, s);
            }
            if (data.park.ContainsKey(string.Format("({0},{1})", s, p)))
            {
                return string.Format("The place ({0},{1}) is occupied", s, p);
            }
            if (data.números.ContainsKey(carro.LicensePlate))
            {
                return string.Format("There is already a vehicle with license plate {0} in the park", carro.LicensePlate);
            }
            data.carros_inpark[carro] = string.Format("({0},{1})", s, p);
            ;
            data.park[string.Format("({0},{1})", s, p)] = carro;
            data.números[carro.LicensePlate] = carro;
            data.d[carro] = t;
            data.ow[carro.Owner].Add(carro);
            data.count[s - 1]--;
            return string.Format("{0} parked successfully at place ({1},{2})", carro.GetType().Name, s, p);
        }

        public string InsertMotorbike(Moto moto, int s, int p, DateTime t)
        {
            if (s > layout.sectors)
            {
                return string.Format("There is no sector {0} in the park", s);
            }
            if (p > layout.places_sec)
            {
                return string.Format("There is no place {0} in sector {1}", p, s);
            }
            if (data.park.ContainsKey(string.Format("({0},{1})", s, p)))
            {
                return string.Format("The place ({0},{1}) is occupied", s, p);
            }
            if (data.números.ContainsKey(moto.LicensePlate))
            {
                return string.Format("There is already a vehicle with license plate {0} in the park", moto.LicensePlate);
            }
            data.carros_inpark[moto] = string.Format("({0},{1})", s, p);
            data.park[string.Format("({0},{1})", s, p)] = moto;
            data.números[moto.LicensePlate] = moto;
            data.d[moto] = t;
            data.ow[moto.Owner].Add(moto);
            data.count[s - 1]++;
            return string.Format("{0} parked successfully at place ({1},{2})", moto.GetType().Name, s, p);
        }

        public string InsertTruck(Truck caminhão, int s, int p, DateTime t)
        {
            if (s > layout.sectors)
            {
                return string.Format("There is no sector {0} in the park", s);
            }
            if (p > layout.places_sec)
            {
                return string.Format("There is no place {0} in sector {1}", p, s);
            }
            if (data.park.ContainsKey(string.Format("({0},{1})", s, p)))
            {
                return string.Format("The place ({0},{1}) is occupied", s, p);
            }
            if (data.números.ContainsKey(caminhão.LicensePlate))
            {
                return string.Format("There is already a vehicle with license plate {0} in the park",
                    caminhão.LicensePlate);
            }
            data.carros_inpark[caminhão] = string.Format("({0},{1})", s, p);
            data.park[string.Format("({0},{1})", s, p)] = caminhão;
            data.números[caminhão.LicensePlate] = caminhão;
            data.d[caminhão] = t;
            data.ow[caminhão.Owner].Add(caminhão);
            return string.Format("{0} parked successfully at place ({1},{2})", caminhão.GetType().Name, s, p);
        }

        public string ExitVehicle(string l_pl, DateTime end, decimal money)
        {
            var vehicle = (data.números.ContainsKey(l_pl)) ? data.números[l_pl] : null;
            if (vehicle == null)
                return string.Format("There is no vehicle with license plate {0} in the park", l_pl);

            var start = data.d[vehicle];
            int endd = (int)Math.Round((end - start).TotalHours);
            var ticket = new StringBuilder();
            ticket.AppendLine(new string('*', 20))
                .AppendFormat("{0}", vehicle.ToString())
                .AppendLine()
                .AppendFormat("at place {0}", data.carros_inpark[vehicle])
                .AppendLine()
                .AppendFormat("Rate: ${0:F2}", (vehicle.ReservedHours * vehicle.RegularRate))
                .AppendLine()
                .AppendFormat("Overtime rate: ${0:F2}",
                    (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0))
                .AppendLine()
                .AppendLine(new string('-', 20))
                .AppendFormat("Total: ${0:F2}",
                    (vehicle.ReservedHours * vehicle.RegularRate +
                     (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)))
                .AppendLine()
                .AppendFormat("Paid: ${0:F2}", money)
                .AppendLine()
                .AppendFormat("Change: ${0:F2}",
                    money -
                    ((vehicle.ReservedHours * vehicle.RegularRate) +
                     (endd > vehicle.ReservedHours ? (endd - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)))
                .AppendLine()
                .Append(new string('*', 20));
            //DELETE
            int sector =
                int.Parse(
                    data.carros_inpark[vehicle].Split(new[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries)[0]);
            data.park.Remove(data.carros_inpark[vehicle]);
            data.carros_inpark.Remove(vehicle);
            data.números.Remove(vehicle.LicensePlate);
            data.d.Remove(vehicle);
            data.ow.Remove(vehicle.Owner, vehicle);
            data.count[sector - 1]--;
            //END OF DELETE
            return ticket.ToString();
        }

        public string GetStatus()
        {
            var places = data.count.Select(
                (sssss, iiiii) => string.Format
                    ("Sector {0}: {1} / {2} ({2}% full)", iiiii + 1, sssss, layout.places_sec,
                        Math.Round((double)sssss / layout.places_sec * 100)));

            return string.Join(Environment.NewLine, places);
        }

        public string FindVehicle(string l_pl)
        {
            var vehicle = (data.números.ContainsKey(l_pl)) ? data.números[l_pl] : null;
            if (vehicle == null)
            {
                return string.Format("There is no vehicle with license plate {0} in the park", l_pl);
            }
            else
            {
                return Input(new[] { vehicle });
            }
        }

        public string FindVehiclesByOwner(string owner)
        {
            if (!data.park.Values.Where(v => v.Owner == owner).Any())
            {
                return string.Format("No vehicles by {0}", owner);
            }

            else
            {
                var found = data.park.Values.ToList();
                var res = found;
                foreach (var f in found)
                {
                    res = res.Where(v => v.Owner == owner).ToList();
                }
                return string.Join(Environment.NewLine, Input(res));
            }
        }

        private string Input(IEnumerable<IVehicle> cars)
        {
            return string.Join
                (Environment.NewLine, cars.Select
                    (vehicle => string.Format("{0}{1}Parked at {2}",
                        vehicle.ToString(), Environment.NewLine, data.carros_inpark[vehicle])));
        }



    }
}



