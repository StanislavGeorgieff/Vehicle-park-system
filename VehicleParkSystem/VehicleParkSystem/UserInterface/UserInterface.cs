using System;
using VehicleParkSystem.Interfaces;

namespace VehicleParkSystem.UserInterface
{
    public class UserInterface : IUserInterface
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string format, params string[] args)
        {
            Console.WriteLine(format, args);
        }
    }
}
