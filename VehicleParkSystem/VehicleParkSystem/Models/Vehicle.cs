

using System.Configuration;

namespace VehicleParkSystem.Models
{
    using System;
    using Interfaces;

    public abstract class Vehicle : IVehicle
    {
        private string licensePlate;
        private string owner;
        private decimal regularRate;
        private decimal overtimeRate;
        private int reservedHours;

        internal Vehicle(string licensePlate, string owner, int reservedHours)
        {
            this.LicensePlate = licensePlate;
            this.Owner = owner;
            this.ReservedHours = reservedHours;
        }

        public string LicensePlate
        {
            get { return licensePlate; }
            set { this.licensePlate = value; }
        }

        public string Owner
        {
            get { return this.owner; }
            set { this.owner = value; }
        }

        public decimal RegularRate
        {
            get { return this.regularRate; }
            set { this.regularRate = value; }
        }

        public decimal OvertimeRate
        {
            get { return this.overtimeRate; }
            set { this.overtimeRate = value; }
        }

        public int ReservedHours
        {
            get { return reservedHours; }
            set { this.reservedHours = value; }
        }
    }
}
