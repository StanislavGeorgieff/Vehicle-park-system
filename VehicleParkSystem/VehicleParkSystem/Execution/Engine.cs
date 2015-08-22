namespace VehicleParkSystem.Execution
{
    using System;
    using VehicleParkSystem.Interfaces;
    using VehicleParkSystem.UserInterface;

    class Engine : IEngine
    {
        private Dispatcher dispatcher;
        private UserInterface userInterface;

        Engine(Dispatcher dispatcher,UserInterface ui)
        {
            this.dispatcher = dispatcher;
            this.userInterface = ui;
        }

        public Engine()
            : this(new Dispatcher(), new UserInterface())
        {
        }

        public void Run()
        {
            while (true)
            {
                string commandLine = this.userInterface.ReadLine();

                if (!string.IsNullOrEmpty(commandLine))
                {
                    try
                    {
                        commandLine = commandLine.Trim();
                        var command = new Command(commandLine);
                        string commandResult = dispatcher.Execution(command);
                        this.userInterface.WriteLine(commandResult);
                    }
                    catch (InvalidOperationException ex)
                    {
                       this.userInterface.WriteLine(ex.Message, "Invalid command");
                    }
                }
            }
        }
    }
}