using SMAModbusConnector.RegisterRead;

// ReSharper disable InconsistentNaming
namespace SMAModbusConnector
{
    public static class RegisterAddresses
    {
        public static readonly RegisterAddress Register_VersionNumber_30001 = new RegisterAddress(30001, DataType.U32,
            new RegisterDescription("de",
                "Versionsnummer des SMA Modbus-Profils"),
            new RegisterDescription("en", "Version number of the SMA Modbus profile"));

        public static readonly RegisterAddress Register_DeviceId_30003 = new RegisterAddress(30003, DataType.U32,
            new RegisterDescription("de",
                "SUSy-ID"),
            new RegisterDescription("en", "Device ID of the WebBox"));

        public static readonly RegisterAddress Register_Serialnumber_30005 = new RegisterAddress(30005, DataType.U32,
            new RegisterDescription("de",
                "Seriennummer"),
            new RegisterDescription("en", "Serialnumber"));

        public static readonly RegisterAddress Register_ModbusDataChangeCount_30007 = new RegisterAddress(30007, DataType.U32,
            new RegisterDescription("de",
                "Modbus-Datenänderung: Zählerwert wird erhöht, wenn neue Daten vorhanden sind."),
            new RegisterDescription("en", "Modbus data change: Counter value will increase if data in the profile has changed."));

        public static readonly RegisterAddress Register_DeviceClass_30051 = new RegisterAddress(30051, DataType.U32,
            new RegisterDescription("de",
                "Geräteklasse"),
            new RegisterDescription("en", "Device class"));

        public static readonly RegisterAddress Register_TotalYieldInWh_30529 = new RegisterAddress(30529, DataType.U32,
            new RegisterDescription("de",
                "Total eingespeiste AC-Energie auf allen Außenleitern (Gesamtertrag), in Wh"),
            new RegisterDescription("en", "Total yield (Wh) [E-Total"));

        public static readonly RegisterAddress Register_TotalYieldInKwh_30531 = new RegisterAddress(30531, DataType.U32,
            new RegisterDescription("de",
                "Total eingespeiste AC-Energie auf allen Außenleitern (Gesamtertrag), in kWh"),
            new RegisterDescription("en", "Total yield (kWh) [E-Total"));

        public static readonly RegisterAddress Register_TotalYieldInMwh_30533 = new RegisterAddress(30533, DataType.U32,
            new RegisterDescription("de",
                "Total eingespeiste AC-Energie auf allen Außenleitern (Gesamtertrag), in MWh"),
            new RegisterDescription("en", "Total yield (MWh) [E-Total"));

        public static readonly RegisterAddress Register_BatteryChargeInPercent_30845 = new RegisterAddress(30845, DataType.U32,
            new RegisterDescription("de",
                "Aktueller Batterieladezustand, in %"),
            new RegisterDescription("en", "Current battery charge status (%) [BatSoc]"));

        public static readonly RegisterAddress Register_BatteryCapacityInPercent_30847 = new RegisterAddress(30847, DataType.U32,
            new RegisterDescription("de",
                "Aktuelle Batteriekapazität, in %"),
            new RegisterDescription("en", "Current battery capacity (%) [Soh]"));

        public static readonly RegisterAddress Register_BatteryTemperatureInC_30849 = new RegisterAddress(30849, DataType.S32,
            new RegisterDescription("de",
                "Batterietemperatur, in °C"),
            new RegisterDescription("en", "Battery temperature (°C) [BatTmp]"));

        public static readonly RegisterAddress Register_PowerGridReferenceInW_30865 = new RegisterAddress(30865, DataType.S32,
            new RegisterDescription("de",
                "Leistung Netzbezug, in W"),
            new RegisterDescription("en", "Power grid reference (W) [GdCsmpPwrAt]"));

        public static readonly RegisterAddress Register_PowerGridFeedInInW_30867 = new RegisterAddress(30867, DataType.S32,
            new RegisterDescription("de",
                "Leistung Netzeinspeisung, in W"),
            new RegisterDescription("en", "Power grid feed-in (W) [GdFeedPwrAt]"));

        public static readonly RegisterAddress Register_PVPowerGeneratedInW_30869 = new RegisterAddress(30869, DataType.S32,
            new RegisterDescription("de",
                "Leistung PV-Erzeugung, in W"),
            new RegisterDescription("en", "PV power generated (W) [TotPvPwr]"));

        public static readonly RegisterAddress Register_CurrentSelfConsumptionInW_30871 = new RegisterAddress(30871, DataType.U32,
            new RegisterDescription("de",
                "Momentaner Eigenverbrauch, in W"),
            new RegisterDescription("en", "Current self-consumption (W) [SlfCsmpPwrAt]"));
    }
}
