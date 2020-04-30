using System;
using System.Net;
using System.Threading;

namespace SMAModbusConnector.TestConsole
{
    class Program
    {
        static void Main()
        {
            // 1. Define all of your SMA products (Ensure, you have enabled Modbus in the device settings)
            var ipSunnyTripower = IPAddress.Parse("192.168.2.61");
            var ipSunnyBoyStorage = IPAddress.Parse("192.168.2.62");

            // 2. Create a connector and add the two devices
            var connector = new Connector();
            connector.TryRegisterDevice(3, ipSunnyTripower, out var sunnyTripowerId);
            connector.TryRegisterDevice(3, ipSunnyBoyStorage, out var sunnyBoyStorageId);

            // 3. Receive a single value
            var batteryChargeInPercent = connector.GetDataForAddress(sunnyBoyStorageId,
                RegisterAddresses.Register_BatteryChargeInPercent_30845);

            // 4. Register multipe register addresses to get periodical data changes
            connector.AddRegisterAddressForDataChanges(sunnyTripowerId,
                RegisterAddresses.Register_CurrentSelfConsumptionInW_30871);

            connector.AddRegisterAddressForDataChanges(sunnyBoyStorageId,
                RegisterAddresses.Register_BatteryChargeInPercent_30845,
                RegisterAddresses.Register_BatteryCapacityInPercent_30847,
                RegisterAddresses.Register_BatteryTemperatureInC_30849);

            // 5. Start data change

            connector.StartDataChange((deviceId, result) =>
            {
                Console.WriteLine(result.FriendlyDescription);
            });

            var mre = new ManualResetEvent(false);
            mre.WaitOne();
        }
    }
}
