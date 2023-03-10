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
            var connector = new Connector
            {
                DataChangeIntervalInSeconds = 3
            };
            connector.TryRegisterDevice(3, ipSunnyTripower, out var sunnyTripowerId);
            connector.TryRegisterDevice(3, ipSunnyBoyStorage, out var sunnyBoyStorageId);

            // 3. Receive a single value

            var batteryCurrentInA = connector.GetDataForAddress(sunnyBoyStorageId,
                RegisterAddresses.Register_BatteryCurrentInAmpere_30843);
            var batteryVoltageInV = connector.GetDataForAddress(sunnyBoyStorageId,
                RegisterAddresses.Register_BatteryVoltageInV_30851);
            var betriebsstatus = connector.GetDataForAddress(sunnyBoyStorageId,
                RegisterAddresses.Register_Betriebsstatus_30955);

            var batteryChargeInPercent = connector.GetDataForAddress(sunnyBoyStorageId,
                RegisterAddresses.Register_BatteryChargeInPercent_30845);

            var powerGridFeedIn =
                connector.GetDataForAddress(sunnyTripowerId, RegisterAddresses.Register_PowerGridFeedInInW_30867);
            var powerGridFeedIn2 =
                connector.GetDataForAddress(sunnyTripowerId, RegisterAddresses.Register_ACActivePowerAcrossAllPhasesInW_30775);
            var powerGridFeedIn3 =
                connector.GetDataForAddress(sunnyTripowerId, RegisterAddresses.Register_PowerGridReferenceInW_30865);

            Console.WriteLine(powerGridFeedIn.FriendlyDescription);
            Console.WriteLine(powerGridFeedIn2.FriendlyDescription);
            Console.WriteLine(powerGridFeedIn3.FriendlyDescription);
            
            Console.ReadLine();
        }
    }
}
