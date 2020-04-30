using SMAModbusConnector.Models;
using SMAModbusConnector.RegisterRead;

// ReSharper disable InconsistentNaming
namespace SMAModbusConnector
{
    public static class RegisterAddresses
    {
        public static readonly RegisterAddress Register_VersionNumber_30001 = new RegisterAddress(30001, DataType.U32,
            new RegisterDescription(Language.German,
                "Versionsnummer des SMA Modbus-Profils"),
            new RegisterDescription(Language.English, "Version number of the SMA Modbus profile"));

        public static readonly RegisterAddress Register_DeviceId_30003 = new RegisterAddress(30003, DataType.U32,
            new RegisterDescription(Language.German,
                "SUSy-ID"),
            new RegisterDescription(Language.English, "Device ID of the WebBox"));

        public static readonly RegisterAddress Register_Serialnumber_30005 = new RegisterAddress(30005, DataType.U32,
            new RegisterDescription(Language.German,
                "Seriennummer"),
            new RegisterDescription(Language.English, "Serialnumber"));

        public static readonly RegisterAddress Register_ModbusDataChangeCount_30007 = new RegisterAddress(30007,
            DataType.U32,
            new RegisterDescription(Language.German,
                "Modbus-Datenänderung: Zählerwert wird erhöht, wenn neue Daten vorhanden sind."),
            new RegisterDescription(Language.English,
                "Modbus data change: Counter value will increase if data in the profile has changed."));

        public static readonly RegisterAddress Register_DeviceClass_30051 = new RegisterAddress(30051, DataType.U32,
            new RegisterDescription(Language.German,
                "Geräteklasse"),
            new RegisterDescription(Language.English, "Device class"));

        public static readonly RegisterAddress Register_TotalYieldInWh_30529 = new RegisterAddress(30529, DataType.U32,
            new RegisterDescription(Language.German,
                "Total eingespeiste AC-Energie auf allen Außenleitern (Gesamtertrag), in Wh"),
            new RegisterDescription(Language.English, "Total yield (Wh) [E-Total"));

        public static readonly RegisterAddress Register_TotalYieldInKwh_30531 = new RegisterAddress(30531, DataType.U32,
            new RegisterDescription(Language.German,
                "Total eingespeiste AC-Energie auf allen Außenleitern (Gesamtertrag), in kWh"),
            new RegisterDescription(Language.English, "Total yield (kWh) [E-Total"));

        public static readonly RegisterAddress Register_TotalYieldInMwh_30533 = new RegisterAddress(30533, DataType.U32,
            new RegisterDescription(Language.German,
                "Total eingespeiste AC-Energie auf allen Außenleitern (Gesamtertrag), in MWh"),
            new RegisterDescription(Language.English, "Total yield (MWh) [E-Total"));

        public static readonly RegisterAddress Register_BatteryChargeInPercent_30845 = new RegisterAddress(30845,
            DataType.U32,
            new RegisterDescription(Language.German,
                "Aktueller Batterieladezustand, in %"),
            new RegisterDescription(Language.English, "Current battery charge status (%) [BatSoc]"));

        public static readonly RegisterAddress Register_BatteryCapacityInPercent_30847 = new RegisterAddress(30847,
            DataType.U32,
            new RegisterDescription(Language.German,
                "Aktuelle Batteriekapazität, in %"),
            new RegisterDescription(Language.English, "Current battery capacity (%) [Soh]"));

        public static readonly RegisterAddress Register_BatteryTemperatureInC_30849 = new RegisterAddress(30849,
            DataType.S32,
            new RegisterDescription(Language.German,
                "Batterietemperatur, in °C"),
            new RegisterDescription(Language.English, "Battery temperature (°C) [BatTmp]"));

        public static readonly RegisterAddress Register_PowerGridReferenceInW_30865 = new RegisterAddress(30865,
            DataType.S32,
            new RegisterDescription(Language.German,
                "Leistung Netzbezug, in W"),
            new RegisterDescription(Language.English, "Power grid reference (W) [GdCsmpPwrAt]"));

        public static readonly RegisterAddress Register_PowerGridFeedInInW_30867 = new RegisterAddress(30867,
            DataType.S32,
            new RegisterDescription(Language.German,
                "Leistung Netzeinspeisung, in W"),
            new RegisterDescription(Language.English, "Power grid feed-in (W) [GdFeedPwrAt]"));

        public static readonly RegisterAddress Register_PVPowerGeneratedInW_30869 = new RegisterAddress(30869,
            DataType.S32,
            new RegisterDescription(Language.German,
                "Leistung PV-Erzeugung, in W"),
            new RegisterDescription(Language.English, "PV power generated (W) [TotPvPwr]"));

        public static readonly RegisterAddress Register_CurrentSelfConsumptionInW_30871 = new RegisterAddress(30871,
            DataType.U32,
            new RegisterDescription(Language.German,
                "Momentaner Eigenverbrauch, in W"),
            new RegisterDescription(Language.English, "Current self-consumption (W) [SlfCsmpPwrAt]"));
    }
}
