
using VehicleParkSystem.Execution;

namespace VehicleParkSystem
{
    internal static class VehicleParkSystemProgramm
    {
        private static void Main()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            
            var engine = new Engine();
            engine.Run();
        }
    }
}
