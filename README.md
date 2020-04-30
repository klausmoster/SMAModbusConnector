# SMAModbusConnector

This is a connector to retrieve data via Modbus TCP from your SMA devices (Sunny Tripower or Sunny Boy Storage, for example).

## Usage

Define your SMA products:

```csharp
var ipSunnyTripower = IPAddress.Parse("192.168.2.61");
var ipSunnyBoyStorage = IPAddress.Parse("192.168.2.62");
```

Create a connector instance and add the devices:

```csharp
var connector = new Connector();
connector.TryRegisterDevice(3, ipSunnyTripower, out var sunnyTripowerId);
connector.TryRegisterDevice(3, ipSunnyBoyStorage, out var sunnyBoyStorageId);
```

You can retrieve a single value (you will find many predefined register addresses in RegisterAddresses. 

```csharp
var batteryChargeInPercent = connector.GetDataForAddress
(
  sunnyBoyStorageId,
  RegisterAddresses.Register_BatteryChargeInPercent_30845
);
```

Or register multiple register addresses for each device.
After starting the periodical data change update, the connector calls your callback function every one second:

```csharp
connector.AddRegisterAddressForDataChanges
(
  sunnyTripowerId,
  RegisterAddresses.Register_CurrentSelfConsumptionInW_30871
);

connector.AddRegisterAddressForDataChanges
(
  sunnyBoyStorageId,
  RegisterAddresses.Register_BatteryChargeInPercent_30845,
  RegisterAddresses.Register_BatteryCapacityInPercent_30847,
  RegisterAddresses.Register_BatteryTemperatureInC_30849
);
```

Start data change:

```csharp
connector.StartDataChange((deviceId, result) =>
{
  // Do something
});
```
 
## License
[MIT](https://choosealicense.com/licenses/mit/)
