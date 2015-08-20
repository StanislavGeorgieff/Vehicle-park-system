using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using VehicleParkSystem.Interfaces;

namespace VehicleParkSystem
{
    public class Command : ICommand
    {
        public Command(string str)
        {
            this.Name = str.Substring(0, str.IndexOf(' '));
            this.Parameters = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(str.Substring(str.IndexOf(' ') + 1));
        }
        public string Name { get; set; }
        public IDictionary<string, string> Parameters { get; set; }

    }
}
