using System;
using System.Net;
using NSubstitute;
using NUnit.Framework;
using SMAModbusConnector.Exceptions;
using SMAModbusConnector.ModbusConnection;
using SMAModbusConnector.RegisterRead;

namespace SMAModbusConnector.Tests
{
    // ReSharper disable once InconsistentNaming
    [TestFixture]
    public class When_adding_registers_to_the_list_of_live_data
    {
        [Test]
        public void The_register_needs_at_least_one_description()
        {
            var modbusConnection = Substitute.For<IModbusConnection>();
            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();
            connector.TryRegisterDevice(3, IPAddress.Any, out var id);

            Assert.Throws<MissingDescriptionAttributeException>(() =>
                connector.AddRegisterAddressForDataChanges(id, new RegisterAddress(0, DataType.S32)));

            Assert.DoesNotThrow(() =>
                connector.AddRegisterAddressForDataChanges(
                    id,
                    new RegisterAddress(0, DataType.S32, new RegisterDescription("en", "Description"))));
        }

        [Test]
        public void A_device_must_be_registered()
        {
            var modbusConnection = Substitute.For<IModbusConnection>();
            var factory = Substitute.For<IModbusConnectionFactory>();
            factory.GetConnection().Returns(modbusConnection);

            Connector.ModbusConnectionFactory = factory;
            var connector = new Connector();
            connector.TryRegisterDevice(1, IPAddress.Any, out var id);

            Assert.Throws<DeviceNotFoundException>(() =>
                connector.AddRegisterAddressForDataChanges(Guid.NewGuid(),
                    new RegisterAddress(0, DataType.S32, new RegisterDescription("en", "Description"))));

            Assert.DoesNotThrow(() =>
                connector.AddRegisterAddressForDataChanges(
                    id,
                    new RegisterAddress(0, DataType.S32, new RegisterDescription("en", "Description"))));
        }
    }
}
