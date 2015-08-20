
using System.Globalization;
using System.Threading;
using VehicleParkSystem;

namespace VehicleParkSystem
{
    internal static class VehicleParkSystemProgramm
    {
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var engine = new Engine();
            engine.Run();
        }
    }
}
