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
            this.Name = commandLine.Substring(0, commandLine.IndexOf(' '));
            this.Parameters = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(commandLine.Substring(commandLine.IndexOf(' ') + 1));
        }
        public string Name { get; set; }
        public IDictionary<string, string> Parameters { get; set; }

    }
}
