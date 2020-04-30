using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using SMAModbusConnector.Exceptions;
using SMAModbusConnector.ModbusConnection;
using SMAModbusConnector.Models;
using SMAModbusConnector.RegisterRead;

//using SMAModbusConnector.Extensions;

namespace SMAModbusConnector
{
    internal class DeviceRegistration
    {
        public byte Unit { get; }
        public IModbusConnection ModbusConnection { get; }
        public RegisterReader ReaderReader { get; }
        public int ModbusDataChangeCount { get; set; }

        public DeviceRegistration(byte unit, IModbusConnection modbusConnection, RegisterReader readerReader)
        {
            Unit = unit;
            ModbusConnection = modbusConnection;
            ReaderReader = readerReader;
            ModbusDataChangeCount = 0;
        }
    }

    public class Connector
    {
        private readonly Dictionary<Guid, List<RegisterAddress>> _registeredAddresses =
            new Dictionary<Guid, List<RegisterAddress>>();

        public Language PreferedDescriptionLanguage { get; set; } = Language.English;

        internal Dictionary<Guid, DeviceRegistration> Devices { get; } = new Dictionary<Guid, DeviceRegistration>();

        internal static IModbusConnectionFactory ModbusConnectionFactory { get; set; } = new ModbusConnectionFactory();

        public bool TryRegisterDevice(byte unit, IPAddress address, out Guid id)
        {
            Console.WriteLine($"Register device with IP-Adress {address} ...");
            id = Guid.Empty;

            try
            {
                var connection = ModbusConnectionFactory.GetConnection();
                connection.Connect(address);
                var registerReader = new RegisterReader(connection);

                id = Guid.NewGuid();
                Devices.Add(id, new DeviceRegistration(unit, connection, registerReader));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public void AddRegisterAddressForDataChanges(Guid deviceId, params RegisterAddress[] registerAddress)
        {
            var firstRegisterAdressWithoutDescription = registerAddress.FirstOrDefault(r => !r.Descriptions.Any());

            if (firstRegisterAdressWithoutDescription != null)
            {
                throw new MissingDescriptionAttributeException();
            }

            if (!Devices.TryGetValue(deviceId, out _))
            {
                throw new DeviceNotFoundException();
            }

            if (!_registeredAddresses.TryGetValue(deviceId, out var registeredAddresses))
            {
                registeredAddresses = new List<RegisterAddress>();
                _registeredAddresses.Add(deviceId, registeredAddresses);
            }

            registeredAddresses.AddRange(registerAddress);
        }

        public void StartDataChange(Action<Guid, Result> callback)
        {
            if (!Devices.Any())
            {
                throw new NoDevicesRegisteredException();
            }

            if (!_registeredAddresses.Any())
            {
                throw new NoRegisterAddressesAddedException();
            }

            Task.Run(async () =>
            {
                var c = callback;

                while (true)
                {
                    await Task.Delay(1000).ConfigureAwait(false);

                    // 1. Query for changes

                    foreach (var key in Devices.Keys)
                    {
                        var modbusDataChangeCountResult =
                            GetDataForAddress(key, RegisterAddresses.Register_ModbusDataChangeCount_30007);
                        var deviceInformation = Devices[key];

                        if (modbusDataChangeCountResult.Value == deviceInformation.ModbusDataChangeCount)
                        {
                            continue;
                        }

                        deviceInformation.ModbusDataChangeCount = modbusDataChangeCountResult.Value;
                        if (_registeredAddresses.TryGetValue(key, out var registerAddresses))
                        {
                            foreach (var registerAddress in registerAddresses)
                            {
                                var resultFor = GetDataForAddress(key, registerAddress);
                                c?.Invoke(key, resultFor);
                            }
                        }
                    }
                }
            });
        }

        public Result GetDataForAddress(Guid deviceId, RegisterAddress registerAddress)
        {
            Console.WriteLine($"Get data for register {registerAddress.Register} for device {deviceId} ...");

            if (!registerAddress.Descriptions.Any())
            {
                throw new MissingDescriptionAttributeException();
            }

            var deviceFound = Devices.TryGetValue(deviceId, out var deviceRegistration);
            if (!deviceFound)
            {
                Console.WriteLine($"\tdevice not found. Use TryRegisterDevice to add a device.");
                throw new DeviceNotFoundException();
            }

            Result result;

            switch (registerAddress.DataType)
            {
                case DataType.S32:
                    var s32Result =
                        deviceRegistration.ReaderReader.ReadS32(deviceRegistration.Unit, registerAddress.Register);
                    result = new Result(deviceId, registerAddress, PreferedDescriptionLanguage, s32Result);
                    break;
                case DataType.U32:
                    var u32Result =
                        deviceRegistration.ReaderReader.ReadU32(deviceRegistration.Unit, registerAddress.Register);
                    result = new Result(deviceId, registerAddress, PreferedDescriptionLanguage, (int) u32Result);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Console.WriteLine("");
            Console.WriteLine(result);
            Console.WriteLine("");

            return result;
        }
    }
}
