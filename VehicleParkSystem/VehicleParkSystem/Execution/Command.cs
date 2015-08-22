using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using VehicleParkSystem.Interfaces;

namespace VehicleParkSystem.Execution
{
    public class Command : ICommand
    {
        public Command(string commandLine)
        {
            this.ParseCommand(commandLine);
        }

        public string Name { get; private set; }

        public IDictionary<string, string> Parameters { get; private set; }

        private void ParseCommand(string commandLine)
        {
            int commandNameEnd = commandLine.IndexOf(' ');
            string commandName = commandLine.Substring(0, commandNameEnd);
            string commandParametersAsString = commandLine.Substring(commandNameEnd + 1);
            var commandParameters = this.ParseCommandParameters(commandParametersAsString);
            this.Name = commandName;
            this.Parameters = commandParameters;
        }

        private IDictionary<string, string> ParseCommandParameters(string commandParametersAsString)
        {
            var serializer = new JavaScriptSerializer();
            var parameters = serializer.Deserialize<Dictionary<string, string>>(commandParametersAsString);
            return parameters;
        }

    }
}
