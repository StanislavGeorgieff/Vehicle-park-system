using System;
using VehicleParkSystem;
using VehicleParkSystem;
using VehicleParkSystem.Interfaces;

namespace VehicleParkSystem
{
    class Engine : IEngine
    {
        private Dispatcher dispatcher;
        Engine(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        public Engine()
            : this(new Dispatcher())
        {
        }

        public void Run()
        {
            while (true)
            {
                string commandLine = Console.ReadLine();
                commandLine.Trim();
              
                if (!string.IsNullOrEmpty(commandLine))
                    try
                    {
                        var command = new Command(commandLine);
                        string commandResult = dispatcher.Execution(command);
                        Console.WriteLine(commandResult);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }
        }
    }
}