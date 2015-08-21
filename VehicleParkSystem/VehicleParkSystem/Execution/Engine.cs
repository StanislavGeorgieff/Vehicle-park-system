using System;
using System.Runtime.InteropServices;
using VehicleParkSystem.Interfaces;

namespace VehicleParkSystem.Execution
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

                if (!string.IsNullOrEmpty(commandLine))
                {
                    try
                    {
                        commandLine = commandLine.Trim();
                        var command = new Command(commandLine);
                        string commandResult = dispatcher.Execution(command);
                        Console.WriteLine(commandResult);
                    }
                    catch (Exception ex )
                    {
                        Console.WriteLine(ex.Message, "Invalid command");
                    }
                }
            }
        }
    }
}