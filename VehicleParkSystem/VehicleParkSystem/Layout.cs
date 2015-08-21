using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleParkSystem
{
    class Layout
    {
        private int numberOfSectors;
        private int placesPerSector;
        public Layout(int numberOfSectors, int placesPerSector)
        {
            
               
                this.NumberOfSectors = numberOfSectors;
            
        
                this.PlacesPerSector = placesPerSector;
            
        }

        public int NumberOfSectors
        {
            get { return this.numberOfSectors; }

            set
            {
                if (value<=0)
                {
                    throw new DivideByZeroException("The number of sectors must be positive.");
                }
                this.numberOfSectors = value;
            }
        }

        public int PlacesPerSector
        {
            get
            {
                return this.placesPerSector;
            }
            set
            {
                if (value <= 0)
                {
                    throw new DivideByZeroException("The number of places per sector must be positive.");
                }
                else
                {
                    this.placesPerSector = value;
                }
            }
        }
    }
}
